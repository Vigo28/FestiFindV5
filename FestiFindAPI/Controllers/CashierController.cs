using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventController>
        /// <summary>
        /// Get all cashiers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Cashier> Get()
        {
            return _context.Cashiers.ToList();
        }

        // GET api/<EventController>/5
        /// <summary>
        /// Get cashier by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Cashier Get(int id)
        {
            return _context.Cashiers.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<EventController>
        /// <summary>
        /// Create new cashier
        /// </summary>
        /// <param name="Name"></param>
        [HttpPost]
        public void Post(string Name)
        {
            Cashier c = new Cashier();
            c.Name = Name;
            _context.Cashiers.Add(c);
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        /// <summary>
        /// Edit cashier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        [HttpPut("{id}")]
        public void Put(int id, string Name)
        {
            Cashier CashierUpdate = _context.Cashiers.FirstOrDefault(c => c.Id == id);
            if (CashierUpdate != null)
            {
                CashierUpdate.Name = Name;
                _context.Update(CashierUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        /// <summary>
        /// Delete cashier
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Cashier CashierRemove = _context.Cashiers.FirstOrDefault(c => c.Id == id);
            _context.Cashiers.Remove(CashierRemove);
            _context.SaveChanges();
        }
    }
}
