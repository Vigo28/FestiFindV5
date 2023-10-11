using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventController>
        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _context.Events.ToList();
        }

        // GET api/<EventController>/5
        /// <summary>
        /// Get event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Event Get(int id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        // POST api/<EventController>
        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="CategoryId"></param>
        /// <param name="Location"></param>
        /// <param name="Date_Time"></param>
        /// <param name="Costs"></param>
        /// <param name="PlacesLeft"></param>
        [HttpPost]
        public void Post(string Name, string Description, int CategoryId, string Location, DateTime Date_Time, float Costs, int PlacesLeft)
        {
            Event e = new Event();
            e.Name = Name; e.Description = Description; e.CategoryId = CategoryId; e.Location = Location; e.Date_Time = Date_Time; e.Costs = Costs; e.PlacesLeft = PlacesLeft;
            _context.Events.Add(e); 
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        /// <summary>
        /// Edit event
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="Category"></param>
        /// <param name="Location"></param>
        /// <param name="Date_Time"></param>
        /// <param name="Costs"></param>
        /// <param name="PlacesLeft"></param>
        [HttpPut("{id}")]
        public void Put(int id, string Name, string Description, int CategoryId, string Location, DateTime Date_Time, float Costs, int PlacesLeft)
        {
            Event EventUpdate = _context.Events.FirstOrDefault(e => e.Id == id);
            if (EventUpdate != null)
            {
                EventUpdate.Name = Name; EventUpdate.Description = Description; EventUpdate.CategoryId = CategoryId; EventUpdate.Location = Location; EventUpdate.Date_Time = Date_Time; EventUpdate.Costs = Costs; EventUpdate.PlacesLeft = PlacesLeft;
                _context.Update(EventUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Event EventRemove = _context.Events.FirstOrDefault(e => e.Id == id);
            _context.Events.Remove(EventRemove); 
            _context.SaveChanges();
        }
    }
}
