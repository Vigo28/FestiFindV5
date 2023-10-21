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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Participant) // Include the Participant navigation property
                .Include(o => o.Event) // Include the Event navigation property
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Participant) // Load the related Participant
                .Include(o => o.Event) // Load the related Event
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/Create
        [Authorize(Roles = "Participant")]
        public IActionResult Create()
        {
            ViewBag.ParticipantId = new SelectList(_context.Participants, "Id", "Name");
            ViewBag.EventId = new SelectList(_context.Events, "Id", "Name");
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParticipantId,EventId,Payed")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParticipantId,EventId,Payed")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context == null)
            {
                return Problem("Entity context is null.");
            }

            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                // Call your cancellation logic here (incrementing available spots and removing the order)
                // and handle the redirection in this action.

                var eventToUpdate = await _context.Events.FindAsync(order.EventId);

                if (eventToUpdate != null)
                {
                    eventToUpdate.IncrementAvailableSpots();
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Handle the case where the associated event doesn't exist
                    return RedirectToAction("ErrorPage");
                }
            }

            return RedirectToAction(nameof(MyOrders));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> MyOrders()
        {
            string participantUsername = User.Identity.Name;
            var participant = _context.Participants.SingleOrDefault(p => p.Name == participantUsername);

            if (participant == null)
            {
                return NotFound(); // Handle the case where the participant doesn't exist.
            }

            var orders = await _context.Orders
                .Include(o => o.Participant)
                .Include(o => o.Event)
                .Where(o => o.ParticipantId == participant.Id) // Filter by participant's ID
                .ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> EventOrders(int id)
        {
            var orders = await _context.Orders
                .Include(o => o.Participant)
                .Include(o => o.Event)
                .Where(o => o.EventId == id) // Filter by EventId
                .ToListAsync();

            return View(orders);
        }

    }
}
