using MediatR;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public record CreateBookCommand : IRequest<string>
    {
        public Guid Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? Publisher { get; init; }
        public bool IsPublished { get; init; }
        public Guid[] AuthorId { get; init; }
    }
}