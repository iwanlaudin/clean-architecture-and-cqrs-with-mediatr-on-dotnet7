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
    public record GetCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;

    public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetCategoriesQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = await (from c in _dbcontext.Categories
                                  where c.IsDeleted != true
                                  select new CategoryDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      CreatedDt = c.CreatedDt,
                                      UpdatedDt = c.UpdatedDt
                                  }).ToListAsync(cancellationToken);
            
            return category;
        }
    }
}