using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Categories.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;
        public CreateCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbcontext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false);

            if (category != null)
            {
                return await Update(category, request, cancellationToken);
            }

            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDt = DateTime.UtcNow,
                UpdatedDt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _dbcontext.Categories.AddAsync(category, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{category.Name} has been saved with ID: {category.Id}";
        }

        public async Task<string> Update(Category category, CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            category.Name = request.Name;
            category.UpdatedDt = DateTime.UtcNow;

            _dbcontext.Categories.Update(category);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{category.Id} has been updated!";
        }
    }
}