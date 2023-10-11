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
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _context.Orders.ToList();
        }

        // GET api/<EventController>/5
        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        // POST api/<EventController>
        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="Payed"></param>
        [HttpPost]
        public void Post(int EventId, int ParticipantId, bool Payed)
        {
            Order o = new Order();
            o.EventId = EventId; o.ParticipantId = ParticipantId; o.Payed = Payed;
            _context.Orders.Add(o);
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        /// <summary>
        /// Edit order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="EventId"></param>
        /// <param name="Payed"></param>
        [HttpPut("{id}")]
        public void Put(int id, int EventId, int ParticipantId, bool Payed)
        {
            Order OrderUpdate = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (OrderUpdate != null)
            {
                OrderUpdate.EventId = EventId; OrderUpdate.ParticipantId = ParticipantId; OrderUpdate.Payed = Payed;

                _context.Update(OrderUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Order OrderRemove = _context.Orders.FirstOrDefault(o => o.Id == id);
            _context.Orders.Remove(OrderRemove);
            _context.SaveChanges();
        }
    }
}
