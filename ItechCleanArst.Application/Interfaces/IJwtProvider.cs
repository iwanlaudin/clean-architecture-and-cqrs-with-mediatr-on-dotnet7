
using ItechCleanArst.Domain.Entities;

namespace ItechCleanArst.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}