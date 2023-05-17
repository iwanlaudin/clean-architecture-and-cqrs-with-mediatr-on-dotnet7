using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Categories.Commands
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<string>;

    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public DeleteCategoryCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _dbcontext.Categories
                .Where(x => x.Id == request.Id && x.IsDeleted != true)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"article with ID {request.Id} not found");

            category.IsDeleted = true;
            _dbcontext.Categories.Update(category);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{category.Id} has been deleted!";
        }
    }
}