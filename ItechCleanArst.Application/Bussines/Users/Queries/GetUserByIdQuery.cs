using ItechCleanArst.Application.Bussines.Users.DTOs;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;

    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IApplicationDbContext _dbcontext;

        public GetUserByIdQueryHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await (from u in _dbcontext.Users
                              where u.Id == request.Id && !u.IsDeleted
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
                              }).FirstOrDefaultAsync(cancellationToken);

            return user ?? throw new KeyNotFoundException("User not found");
        }
    }
}