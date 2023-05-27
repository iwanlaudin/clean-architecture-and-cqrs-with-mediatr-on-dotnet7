
using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Users.Commands
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public CreateUserCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbcontext.Users
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user is not null)
            {
                return await Update(user, request, cancellationToken);
            }

            user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _dbcontext.Users.AddAsync(user, cancellationToken);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{user.Id} has been created";
        }

        public async Task<string> Update(User user, CreateUserCommand request, CancellationToken cancellationToken)
        {
           user.FirstName = request.FirstName;
           user.LastName = request.LastName;
           user.Email = request.Email;
           user.Username = request.Username;
           user.UpdatedAt = DateTime.UtcNow;

           if (request.Password is not null)
           {
               user.Password = request.Password;
           }

           _dbcontext.Users.Update(user);
           await _dbcontext.SaveChangesAsync(cancellationToken);

           return $"{user.Id} has been updated";
        }
    }
}