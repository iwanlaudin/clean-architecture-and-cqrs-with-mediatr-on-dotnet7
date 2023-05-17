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
                                  where c.Id == request.Id && !c.IsDeleted
                                  select new CategoryDto
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      CreatedDt = c.CreatedAt,
                                      UpdatedDt = c.UpdatedAt
                                  }
            ).FirstOrDefaultAsync(cancellationToken);

            return category ?? throw new KeyNotFoundException("Category not found");
        }
    }
}