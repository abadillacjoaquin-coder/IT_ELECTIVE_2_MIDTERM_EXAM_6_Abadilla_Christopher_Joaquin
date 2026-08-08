using Package_Pickup_Monitoring_System.Models;

namespace Package_Pickup_Monitoring_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new()
        {
            new User { Id = 1, FirstName = "Admin", LastName = "Staff", Email = "admin@building.com", Username = "admin", Password = "password123" }
        };

        public User? GetByUsername(string username) =>
            _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        public void Add(User user)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }

        public bool ValidateCredentials(string username, string password) =>
            _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Password == password);
    }
}