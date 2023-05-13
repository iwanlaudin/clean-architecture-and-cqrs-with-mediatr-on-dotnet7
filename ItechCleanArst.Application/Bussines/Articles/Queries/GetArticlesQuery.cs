
using ItechCleanArst.Application.Bussines.Articles.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Articles.Queries
{
    public record GetArticlesQuery() : IRequest<IEnumerable<ArticleDto>>;

    public sealed class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, IEnumerable<ArticleDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetArticlesQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await (
                from a in _dbcontext.Articles
                join c in _dbcontext.Categories on a.CategoryId equals c.Id
                where a.IsDeleted != true && c.IsDeleted != true
                select new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    PublishedDate = a.PublishedDate,
                    TotalUserRate = a.TotalUserRate,
                    CreatedDt = a.CreatedAt,
                    UpdatedDt = a.UpdatedAt,
                    Category = new Categories.DTOs.CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreatedDt = c.CreatedAt,
                        UpdatedDt = c.UpdatedAt
                    }
                }
            ).ToListAsync(cancellationToken);

            return articles;
        }
    }
}