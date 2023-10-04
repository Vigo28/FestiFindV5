using FestFindV2.Models;
using FestiFindV5.Data;
using FestiFindV5.Models;
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
            // Fetch event data from your database using Entity Framework
            var events = _context.Events
                .OrderBy(e => e.Date_Time) // Sort events by date in ascending order
                .ToList(); // Convert to a list

            return View(events);
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