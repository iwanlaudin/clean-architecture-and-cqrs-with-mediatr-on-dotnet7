using ItechCleanArst.Application.Bussines.Authors.DTOs;
using ItechCleanArst.Application.Bussines.Books.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Queries
{
    public record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;

    public sealed class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetBookByIdQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await (from b in _dbcontext.Books
                              join ba in _dbcontext.BookAuthors on b.Id equals ba.BookId into authors
                              where b.Id == request.Id && !b.IsDeleted
                              select new BookDto
                              {
                                  Id = b.Id,
                                  Title = b.Title,
                                  Description = b.Description,
                                  Publisher = b.Publisher,
                                  PublishedDate = b.PublishedDate,
                                  Author = authors.Where(a => !a.Author.IsDeleted).Select(a => new AuthorDto
                                  {
                                      Id = a.Id,
                                      FirstName = a.Author.FirstName,
                                      LastName = a.Author.LastName,
                                      Email = a.Author.Email,
                                      Address = a.Author.Address,
                                      CreatedAt = a.Author.CreatedAt,
                                      UpdatedAt = a.Author.UpdatedAt
                                  }).ToList()
                              }
            ).FirstOrDefaultAsync(cancellationToken);

            return book ?? throw new KeyNotFoundException("Book not found");
        }
    }
}