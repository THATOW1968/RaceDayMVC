using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using System.Diagnostics;

namespace RaceDayMVC.Controllers
{//start of namespace
    public class HomeController : Controller
    {//start of class
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }//end of class
}//end of namespace

