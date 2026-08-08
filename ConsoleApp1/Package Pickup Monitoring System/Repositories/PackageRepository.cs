using Package_Pickup_Monitoring_System.Models;

namespace Package_Pickup_Monitoring_System.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private static readonly List<Package> _packages = new()
        {
            new Package
            {
                Id = 1,
                TrackingNumber = "TRO1130",
                RecipientName = "Cj Abadilla",
                UnitNumber = "Apt 4B",
                ContactNumber = "623-1391",
                CourierCompany = "Lalamove",
                PackageType = "Box",
                ArrivalDateTime = DateTime.Now.AddHours(-5),
                ExpectedPickupDate = DateTime.Now.AddDays(1),
                ReceivedBy = "Admin",
                Status = PackageStatus.WaitingForPickup,
                Notes = "Fragile handle with care"
            }
        };

        public IEnumerable<Package> GetAll() => _packages.OrderByDescending(p => p.ArrivalDateTime);

        public Package? GetById(int id) => _packages.FirstOrDefault(p => p.Id == id);

        public void Add(Package package)
        {
            package.Id = _packages.Count > 0 ? _packages.Max(p => p.Id) + 1 : 1;
            _packages.Add(package);
        }

        public void Update(Package package)
        {
            var existing = GetById(package.Id);
            if (existing != null)
            {
                existing.TrackingNumber = package.TrackingNumber;
                existing.RecipientName = package.RecipientName;
                existing.UnitNumber = package.UnitNumber;
                existing.ContactNumber = package.ContactNumber;
                existing.CourierCompany = package.CourierCompany;
                existing.PackageType = package.PackageType;
                existing.ExpectedPickupDate = package.ExpectedPickupDate;
                existing.ClaimedDateTime = package.ClaimedDateTime;
                existing.ReceivedBy = package.ReceivedBy;
                existing.Status = package.Status;
                existing.Notes = package.Notes;
            }
        }

        public IEnumerable<Package> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAll();

            searchTerm = searchTerm.ToLower();
            return _packages.Where(p =>
                p.TrackingNumber.ToLower().Contains(searchTerm) ||
                p.RecipientName.ToLower().Contains(searchTerm) ||
                p.CourierCompany.ToLower().Contains(searchTerm) ||
                p.UnitNumber.ToLower().Contains(searchTerm)
            ).OrderByDescending(p => p.ArrivalDateTime);
        }
    }
}