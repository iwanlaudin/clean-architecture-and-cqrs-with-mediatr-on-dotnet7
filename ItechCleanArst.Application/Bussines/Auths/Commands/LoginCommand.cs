using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Auths.Commands
{
    public record LoginCommand(string Email) : IRequest<string>;

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(IApplicationDbContext dbcontext, IJwtProvider jwtProvider)
        {
            _dbcontext = dbcontext;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbcontext.Users
                .Where(u => u.Email == request.Email && !u.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}