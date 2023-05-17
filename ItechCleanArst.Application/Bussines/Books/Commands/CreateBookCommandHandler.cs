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
                .Include(x => x.BookAuthors)
                .Where(x => x.Id == request.Id && x.IsDeleted == false)
                .FirstOrDefaultAsync(cancellationToken);

            if (book != null)
            {
                return await Update(book, request, cancellationToken);
            }

            book = new Book
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Publisher = request.Publisher,
                PublishedDate = request.IsPublished ? DateTime.UtcNow : null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            book.BookAuthors = request.AuthorId != null
                              ? request.AuthorId.Select(authorId => new BookAuthor
                              {
                                  Id = Guid.NewGuid(),
                                  BookId = book.Id,
                                  AuthorId = authorId,
                              }).ToList()
                              : new List<BookAuthor>();

            await _dbcontext.Books.AddAsync(book, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{book.Title} has been saved!";
        }

        public async Task<string> Update(Book book, CreateBookCommand request, CancellationToken cancellationToken)
        {
            book.Title = request.Title;
            book.Description = request.Description;
            book.Publisher = request.Publisher;

            if (request.IsPublished)
            {
                book.PublishedDate = DateTime.UtcNow;
            }

            if (request.AuthorId != null && request.AuthorId.Any())
            {
                book.BookAuthors = request.AuthorId.Select(authorId => new BookAuthor
                {
                    AuthorId = authorId
                }).ToList();
            }

            _dbcontext.Books.Update(book);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{book.Title} has been updated!";
        }
    }
}