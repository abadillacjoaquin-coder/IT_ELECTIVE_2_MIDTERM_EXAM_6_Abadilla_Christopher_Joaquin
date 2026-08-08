using Package_Pickup_Monitoring_System.Models;

namespace Package_Pickup_Monitoring_System.Repositories
{
    public interface IPackageRepository
    {
        IEnumerable<Package> GetAll();
        Package? GetById(int id);
        void Add(Package package);
        void Update(Package package);
        IEnumerable<Package> Search(string searchTerm);
    }
}