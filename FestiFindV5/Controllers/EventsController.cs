using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Authorization;

namespace FestiFindV5.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            // Fetch the list of categories from your data source (replace _context and Categories with your actual data context and model).
            var categories = _context.Category.ToList();

            // Create a SelectList containing category names.
            var categoryList = new SelectList(categories, "Id", "Name");

            // Pass the SelectList to the view using ViewBag.
            ViewBag.CategoryList = categoryList;

            return _context.Events != null ? 
                          View(await _context.Events.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            var categories = _context.Category.ToList();

            // Create a SelectList containing category names.
            var categoryList = new SelectList(categories, "Id", "Name");

            // Pass the SelectList to the view using ViewBag.
            ViewBag.CategoryList = categoryList;
            return View(@event);
        }
        [Authorize(Roles = "Organizer")]

        // GET: Events/Create
        public IActionResult Create()
        {
            // Fetch the list of categories from your data source (replace _context and Categories with your actual data context and model).
            var categories = _context.Category.ToList();

            // Create a SelectList containing category names.
            var categoryList = new SelectList(categories, "Id", "Name");

            // Pass the SelectList to the view using ViewBag.
            ViewBag.CategoryList = categoryList;

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Location,Date_Time,Costs,PlacesLeft")] Event @event)
        {
            // Get the username of the logged-in user (organizer)
            string organizerUsername = User.Identity.Name;

            // Assuming you have a data store where you associate organizers with usernames
            var organizer = _context.Organizers.SingleOrDefault(o => o.Name == organizerUsername);

            if (organizer != null)
            {
                // Set the CategoryId based on the form input
                @event.CategoryId = Convert.ToInt32(Request.Form["CategoryId"]);

                // Associate the OrganizerID with the event
                @event.OrganizerId = organizer.Id;

                // Add the event to the context
                _context.Add(@event);
                await _context.SaveChangesAsync();

                // Redirect to a suitable page or action
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the case where the user is not an organizer
                // You can return an error message or redirect to a page, as needed
                return RedirectToAction("ErrorPage");
            }
        }


        // GET: Events/Edit/5
        [Authorize(Roles = "Organizer")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CategoryId,Location,Date_Time,Costs,PlacesLeft")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }
            string organizerUsername = User.Identity.Name;
            var organizer = _context.Organizers.SingleOrDefault(o => o.Name == organizerUsername);
            // Associate the OrganizerID with the event
            @event.OrganizerId = organizer.Id;
            _context.Update(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Organizer")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> CreateOrder(int eventId)
        {
            // Get the username of the logged-in user
            string username = User.Identity.Name;

            // Assuming you have a data store where you associate participants with usernames
            var participant = _context.Participants.SingleOrDefault(p => p.Name == username);

            if (participant != null)
            {
                var order = new Order
                {
                    ParticipantId = participant.Id,
                    EventId = eventId,
                };

                var eventToUpdate = await _context.Events.FindAsync(order.EventId); // Await here to get the actual Event object.

                if (eventToUpdate != null) // Check if the event exists.
                {
                    eventToUpdate.ReduceAvailableSpots();
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync(); // Await SaveChangesAsync() for async database operation.

                    // Redirect to the Order Detail page for the newly created order
                    return RedirectToAction("Details", "Orders", new { id = order.Id });
                }
                else
                {
                    // Handle the case where the event doesn't exist
                    return RedirectToAction("ErrorPage");
                }
            }
            else
            {
                // Handle the case where the user is not a participant
                return RedirectToAction("ErrorPage");
            }
        }
        public async Task<IActionResult> MyEvents()
        {
            string organizerUsername = User.Identity.Name;
            var organizer = _context.Organizers.SingleOrDefault(o => o.Name == organizerUsername);

            if (organizer == null)
            {
                return NotFound(); // Handle the case where the organizer doesn't exist.
            }

            // Fetch the events associated with the organizer.
            var events = _context.Events.Where(e => e.OrganizerId == organizer.Id).ToList();

            // You can also include the category list in a similar way if needed.
            var categories = _context.Category.ToList();
            var categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.CategoryList = categoryList;

            return View(events);
        }
    }
}
