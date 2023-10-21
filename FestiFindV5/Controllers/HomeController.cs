using FestFindV2.Models;
using FestiFindV5.Data;
using FestiFindV5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FestiFindV5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the current date and time
            DateTime currentDate = DateTime.Now;

            // Fetch event data and include category information using a join
            var upcomingEventsWithCategories = _context.Events
                .Join(_context.Category,
                    e => e.CategoryId,
                    c => c.Id,
                    (e, c) => new
                    {
                        Event = e,
                        Category = c
                    })
                .Where(ec => ec.Event.Date_Time > currentDate) // Filter for upcoming events
                .OrderBy(ec => ec.Event.Date_Time)
                .ToList();

            return View(upcomingEventsWithCategories);
        }



        public IActionResult Calendar(string month)
        {
            DateTime currentDate = DateTime.Today;
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            if (!string.IsNullOrWhiteSpace(month))
            {
                if (DateTime.TryParseExact(month, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out var selectedDate))
                {
                    // Handle the selected month here if needed
                    // For example, update the currentDate to the selectedDate
                    currentDate = selectedDate;
                    daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                }
            }
            List<Event> events = _context.Events.ToList();
            return View(events);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}