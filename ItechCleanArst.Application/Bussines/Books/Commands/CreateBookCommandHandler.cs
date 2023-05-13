using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _dbcontext;
        public CreateBookCommandHandler(IApplicationDbContext dbcontext, IMediator mediator)
        {
            _dbcontext = dbcontext;
            _mediator = mediator;
        }

        public async Task<string> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbcontext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false, cancellationToken);

            if (book != null)
            {
                return await Update(book, request, cancellationToken);
            }

            await using var transaction = _dbcontext.Database.BeginTransaction();
            try
            {
                book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Description = request.Description,
                    Publisher = request.Publisher,
                    PublishedDate = request.PublishedDate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                await _dbcontext.Books.AddAsync(book, cancellationToken);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                await _mediator.Send(new CreateBookAuthorCommand(book.Id, request.AuthorId));

                await _dbcontext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                _dbcontext.Database.RollbackTransaction();
                throw ex;
            }

            return $"{book.Title} has been saved!";
        }

        public async Task<string> Update(Book book, CreateBookCommand request, CancellationToken cancellationToken)
        {
            book.Title = request.Title;
            book.Description = request.Description;
            book.Publisher = request.Publisher;

            if (request.PublishedDate != null)
            {
                book.PublishedDate = request.PublishedDate;
            }

            if (request.AuthorId != null && request.AuthorId?.Length > 0)
            {
                await _mediator.Send(new CreateBookAuthorCommand(book.Id, request.AuthorId));
            }

            _dbcontext.Books.Update(book);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{book.Title} has been updated!";
        }
    }
}