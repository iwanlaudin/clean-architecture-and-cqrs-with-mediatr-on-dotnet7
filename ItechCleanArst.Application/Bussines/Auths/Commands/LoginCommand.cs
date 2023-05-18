using ItechCleanArst.Application.Interfaces;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Auths.Commands
{
    public record LoginCommand(string Email) : IRequest<string>;

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public LoginCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException(); 
        }
    }
}