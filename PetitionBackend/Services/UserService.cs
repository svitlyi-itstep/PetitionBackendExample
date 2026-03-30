using PetitionBackend.DbContexts;
using PetitionBackend.Models;

namespace PetitionBackend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();
        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void RegisterUser(UserRegisterDTO user)
        {
            Add(new User { 
                Username = user.Username, 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = user.Role,
            });
        }

        public User? ValidateUser(UserLoginDTO credentials)
        {
            var user = _context.Users.FirstOrDefault(
                u => u.Username == credentials.Username);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
