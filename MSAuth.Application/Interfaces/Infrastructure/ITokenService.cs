using MSAuth.Domain.Entities;

namespace MSAuth.Application.Interfaces.Infrastructure
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
