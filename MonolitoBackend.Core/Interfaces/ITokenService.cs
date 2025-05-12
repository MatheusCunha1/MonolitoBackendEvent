using MonolitoBackend.Core.Entidade;

namespace MonolitoBackend.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
