
using ItechCleanArst.Application.Bussines.Users.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Users.Queries
{
    public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;

    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetUsersQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await (from u in _dbcontext.Users
                               where !u.IsDeleted
                               select new UserDto
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   Username = u.Username,
                                   Password = u.Password,
                                   CreatedAt = u.CreatedAt,
                                   UpdatedAt = u.UpdatedAt
                               }).ToListAsync(cancellationToken);

            return users;
        }
    }
}