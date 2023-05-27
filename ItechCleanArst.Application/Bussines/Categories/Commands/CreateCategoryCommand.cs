using MediatR;

namespace ItechCleanArst.Application.Bussines.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<string>
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
    }
}