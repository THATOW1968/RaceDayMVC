using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{//start of namespace
    public class AccountController : Controller
    {//start of class
        private readonly ApiService _api;

        public AccountController(ApiService api) { _api = api; }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var success = await _api.LoginAsync(email, password);

            if (success)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            var success = await _api.RegisterAsync(newUser);

            if (success)
                return RedirectToAction("Login");

            ViewBag.Error = "Registration failed. Email might already exist.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }//end of class
}//end of namespace

