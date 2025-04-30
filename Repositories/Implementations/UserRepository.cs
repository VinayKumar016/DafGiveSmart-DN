using CDTApi.Models;
using CDTApi.Repositories;
namespace CDTApi.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly JsonDataContext _context;

        public UserRepository(JsonDataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Data.Users;
        }

        public User? GetByEmail(string email)
        {
            return _context.Data.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            user.UserId = _context.Data.Users.Any()
                ? _context.Data.Users.Max(u => u.UserId) + 1
                : 1;

            _context.Data.Users.Add(user);
            _context.SaveChanges();
        }
    }

}
