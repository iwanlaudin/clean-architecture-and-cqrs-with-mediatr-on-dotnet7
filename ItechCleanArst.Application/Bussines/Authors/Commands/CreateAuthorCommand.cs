using MediatR;

namespace ItechCleanArst.Application.Bussines.Authors.Commands
{
    public record CreateAuthorCommand : IRequest<string>
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Address { get; init; }
    }
}