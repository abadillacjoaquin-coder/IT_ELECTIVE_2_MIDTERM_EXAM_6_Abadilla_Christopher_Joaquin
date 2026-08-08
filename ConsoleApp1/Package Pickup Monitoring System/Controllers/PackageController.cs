using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Package_Pickup_Monitoring_System.Models;
using Package_Pickup_Monitoring_System.Repositories;

namespace Package_Pickup_Monitoring_System.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        private readonly IPackageRepository _packageRepository;

        public PackageController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public IActionResult Index(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;
            var packages = _packageRepository.Search(searchTerm);
            return View(packages);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Package
            {
                ArrivalDateTime = DateTime.Now,
                ReceivedBy = User.Identity?.Name ?? "Staff"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            if (!ModelState.IsValid) return View(package);

            _packageRepository.Add(package);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var package = _packageRepository.GetById(id);
            if (package == null) return NotFound();
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Package package)
        {
            if (!ModelState.IsValid) return View(package);

            _packageRepository.Update(package);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var package = _packageRepository.GetById(id);
            if (package == null) return NotFound();
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Claim(int id)
        {
            var package = _packageRepository.GetById(id);
            if (package != null && package.Status == PackageStatus.WaitingForPickup)
            {
                package.Status = PackageStatus.Claimed;
                package.ClaimedDateTime = DateTime.Now;
                _packageRepository.Update(package);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}