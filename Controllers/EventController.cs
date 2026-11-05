using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{//start of namespace
    public class EventController : Controller
    {//start of class
        private readonly ApiService _api;

        public EventController(ApiService api) { _api = api; }

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("UserRole") == null)
            {
                context.Result = RedirectToAction("Login", "Account");
            }
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            var events = await _api.GetEventsAsync();
            return View(events);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organizer") return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organizer") return RedirectToAction("Index");

            // Simulated Azure Blob Storage image
            newEvent.ImageUrl = "https://images.unsplash.com/photo-1530549387789-4c1017266635?q=80&w=1000&auto=format&fit=crop";

            var success = await _api.CreateEventAsync(newEvent);
            if (success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Failed to create event.";
            return View(newEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int eventId)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId")!);
            await _api.EnrollAsync(eventId, userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewEnrolments(int eventId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organizer") return RedirectToAction("Index");
            var enrolments = await _api.GetEventEnrollmentsAsync(eventId);
            return View(enrolments);
        }
    }//end of class
}//end of namespace
