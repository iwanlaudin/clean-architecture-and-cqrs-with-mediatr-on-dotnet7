using MediatR;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public record CreateBookAuthorCommand(Guid BookId, Guid[]? AuthorId) : IRequest<Guid>;
}