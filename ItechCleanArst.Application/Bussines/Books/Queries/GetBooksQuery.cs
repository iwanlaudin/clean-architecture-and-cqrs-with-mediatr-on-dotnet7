using ItechCleanArst.Application.Bussines.Authors.DTOs;
using ItechCleanArst.Application.Bussines.Books.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Queries
{
    public record GetBooksQuery() : IRequest<IEnumerable<BookDto>>;

    public sealed class GetBookQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetBookQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var book = await (from b in _dbcontext.Books
                              join ba in _dbcontext.BookAuthors on b.Id equals ba.BookId into authors
                              where !b.IsDeleted
                              select new BookDto
                              {
                                  Id = b.Id,
                                  Title = b.Title,
                                  Description = b.Description,
                                  Publisher = b.Publisher,
                                  PublishedDate = b.PublishedDate,
                                  Author = authors.Where(a => !a.Author.IsDeleted).Select(a => new AuthorDto
                                  {
                                      Id = a.Author.Id,
                                      FirstName = a.Author.FirstName,
                                      LastName = a.Author.LastName,
                                      Email = a.Author.Email,
                                      Address = a.Author.Address,
                                      CreatedAt = a.Author.CreatedAt,
                                      UpdatedAt = a.Author.UpdatedAt
                                  }).ToList()
                              }).ToListAsync(cancellationToken);

            return book;
        }
    }
}