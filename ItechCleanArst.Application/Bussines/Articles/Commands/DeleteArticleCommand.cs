using MediatR;

namespace ItechCleanArst.Application.Bussines.Articles.Commands
{
    public record DeleteArticleCommand(Guid Id) : IRequest<string>;
}