using MediatR;

namespace ItechCleanArst.Application.Bussines.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}