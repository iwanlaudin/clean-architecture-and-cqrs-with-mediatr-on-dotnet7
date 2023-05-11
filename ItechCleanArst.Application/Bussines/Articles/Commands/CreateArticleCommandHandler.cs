using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Articles.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;
        public CreateArticleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<string> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _dbcontext.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false);

            if (article != null)
            {
                return await Update(article, request, cancellationToken);
            }

            article = new Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                CategoryId = request.CategoryId,
                CreatedDt = DateTime.UtcNow,
                UpdatedDt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _dbcontext.Articles.AddAsync(article, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{article.Title} has been saved with ID: {article.Id}";
        }

        public async Task<string> Update(Article article, CreateArticleCommand request, CancellationToken cancellationToken)
        {
            article.Title = request.Title;
            article.Content = request.Content;
            article.UpdatedDt = DateTime.UtcNow;
            article.CategoryId = request.CategoryId;

            _dbcontext.Articles.Update(article);
            await _dbcontext.SaveChangesAsync();

            return $"{article.Id} has been updated!";
        }
    }
}