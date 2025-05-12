using MonolitoBackend.Core.Entidade;

namespace MonolitoBackend.Core.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task<User?> GetUserByUserName(string userName);
        Task<bool> UserExistsByEmail(string email);
    }
}
