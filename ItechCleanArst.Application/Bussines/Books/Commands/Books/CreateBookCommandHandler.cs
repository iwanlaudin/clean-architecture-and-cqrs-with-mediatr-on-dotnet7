using ItechCleanArst.Application.Bussines.Books.Commands.BookAuthors;
using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _dbcontext;
        public CreateBookCommandHandler(IApplicationDbContext dbcontext, IMediator mediator)
        {
            _dbcontext = dbcontext;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbcontext.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == true, cancellationToken);

            if (book != null)
            {
                throw new Exception();
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
                    PublisherDate = request.PublisherDate,
                    CreatedDt = DateTime.Now,
                    UpdatedDt = DateTime.Now,
                    IsDeleted = false
                };

                await _dbcontext.Books.AddAsync(book, cancellationToken);
                await _dbcontext.SaveChangesAsync(cancellationToken);

                await _mediator.Send(new CreateBookAuthorCommand
                {
                    BookId = book.Id,
                    AuthorId = request.AuthorId
                });

                await _dbcontext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _dbcontext.Database.RollbackTransaction();

                throw ex;
            }


            return book.Id;
        }
    }
}