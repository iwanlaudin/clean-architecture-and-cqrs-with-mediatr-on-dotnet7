using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechCleanArst.Application.Bussines.Categories.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;

    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetCategoryByIdQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await (from c in _dbcontext.Categories
                                  where c.Id == request.Id && c.IsDeleted != true
                                  select new CategoryDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      CreatedDt = c.CreatedDt,
                                      UpdatedDt = c.UpdatedDt
                                  }
            ).FirstOrDefaultAsync(cancellationToken);

            return category;
        }
    }
}