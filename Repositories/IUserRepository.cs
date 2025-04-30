using CDTApi.Models;

namespace CDTApi.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetByEmail(string email);
        void AddUser(User user);
    }
}
