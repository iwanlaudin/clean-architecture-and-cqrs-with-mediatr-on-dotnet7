using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Articles.Commands
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public DeleteArticleCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _dbcontext.Articles
                .Where(x => x.Id == request.Id && x.IsDeleted != true)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"article with ID {request.Id} not found");

            article.IsDeleted = true;
            _dbcontext.Articles.Update(article);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{article.Title} has been deleted!";
        }
    }
}