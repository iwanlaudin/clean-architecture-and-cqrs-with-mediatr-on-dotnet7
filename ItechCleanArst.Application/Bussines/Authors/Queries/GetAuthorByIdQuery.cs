using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechCleanArst.Application.Bussines.Authors.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Authors.Queries
{
    public record GetAuthorByIdQuery(Guid Id) : IRequest<AuthorDto>;

    public sealed class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetAuthorByIdQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await (from a in _dbcontext.Authors
                                where a.Id == request.Id && a.IsDeleted != true
                                select new AuthorDto
                                {
                                    Id = a.Id,
                                    FirstName = a.FirstName,
                                    LastName = a.LastName,
                                    Email = a.Email,
                                    Address = a.Address,
                                    CreatedAt = a.CreatedAt,
                                    UpdatedAt = a.UpdatedAt
                                }).FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}