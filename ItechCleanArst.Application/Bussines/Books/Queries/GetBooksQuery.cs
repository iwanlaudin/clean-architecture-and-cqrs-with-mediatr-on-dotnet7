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
                              where b.IsDeleted != true
                              select new BookDto
                              {
                                  Id = b.Id,
                                  Title = b.Title,
                                  Description = b.Description,
                                  Publisher = b.Publisher,
                                  PublishedDate = b.PublishedDate,
                                  Author = (from ba in _dbcontext.BookAuthors
                                            join a in _dbcontext.Authors on ba.AuthorId equals a.Id
                                            where ba.BookId == b.Id && a.IsDeleted != true
                                            select new AuthorDto
                                            {
                                                Id = a.Id,
                                                FirstName = a.FirstName,
                                                LastName = a.LastName,
                                                Email = a.Email,
                                                Address = a.Address,
                                                CreatedAt = a.CreatedAt,
                                                UpdatedAt = a.UpdatedAt
                                            }).ToList()
                              }).ToListAsync(cancellationToken);

            return book;
        }
    }
}