using Package_Pickup_Monitoring_System.Models;

namespace Package_Pickup_Monitoring_System.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        void Add(User user);
        bool ValidateCredentials(string username, string password);
    }
}