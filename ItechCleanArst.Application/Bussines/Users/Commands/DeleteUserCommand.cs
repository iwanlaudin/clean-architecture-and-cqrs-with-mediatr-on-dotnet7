using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Users.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest<string>;

    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public DeleteUserCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await (from u in _dbcontext.Users
                             where u.Id == request.Id && !u.IsDeleted
                             select u).FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.IsDeleted = true;

            _dbcontext.Users.Update(user);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{user.Id} has been deleted!";
        }
    }
}