using ItechCleanArst.Application.Bussines.Articles.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Articles.Queries
{
    public record GetArticleByIdQuery(Guid Id) : IRequest<ArticleDto>;

    public sealed class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetArticleByIdQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await (from a in _dbcontext.Articles
                                join c in _dbcontext.Categories on a.CategoryId equals c.Id
                                where a.Id == request.Id && a.IsDeleted != true
                                select new ArticleDto
                                {
                                    Id = a.Id,
                                    Title = a.Title,
                                    Content = a.Content,
                                    CreatedDt = a.CreatedDt,
                                    UpdatedDt = a.UpdatedDt,
                                    Category = new Categories.DTOs.CategoryDto
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        CreatedDt = c.CreatedDt,
                                        UpdatedDt = c.UpdatedDt
                                    }
                                }).FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}