using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Package_Pickup_Monitoring_System.Models;
using Package_Pickup_Monitoring_System.Models.ViewModels;
using Package_Pickup_Monitoring_System.Repositories;
using System.Security.Claims;

namespace Package_Pickup_Monitoring_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_userRepository.ValidateCredentials(model.Username, model.Password))
            {
                var user = _userRepository.GetByUsername(model.Username);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user!.Username),
                    new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Package");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid) return View(user);

            if (_userRepository.GetByUsername(user.Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(user);
            }

            _userRepository.Add(user);
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}