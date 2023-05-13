
using ItechCleanArst.Application.Bussines.Authors.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Authors.Queries
{
    public record GetAuthorsQuery() : IRequest<IEnumerable<AuthorDto>>;

    public sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetAuthorsQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var result = await (from a in _dbcontext.Authors
                                where a.IsDeleted != true
                                select new AuthorDto
                                {
                                    Id = a.Id,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    Email = a.Email,
                                    Address = a.Address,
                                    CreatedAt = a.CreatedAt,
                                    UpdatedAt = a.UpdatedAt
                                }).ToListAsync(cancellationToken);

            return result;
        }
    }
}