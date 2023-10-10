using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventController>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post(int EventId, bool Payed)
        {
            Order o = new Order();
            o.EventId = EventId; o.Payed = Payed;
            _context.Orders.Add(o);
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, int EventId, bool Payed)
        {
            Order OrderUpdate = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (OrderUpdate != null)
            {
                OrderUpdate.EventId = EventId; OrderUpdate.Payed = Payed;
                _context.Update(OrderUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Order OrderRemove = _context.Orders.FirstOrDefault(o => o.Id == id);
            _context.Orders.Remove(OrderRemove);
            _context.SaveChanges();
        }
    }
}
