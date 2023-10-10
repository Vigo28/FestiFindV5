using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipantController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventController>
        [HttpGet]
        public IEnumerable<Participant> Get()
        {
            return _context.Participants.ToList();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Participant Get(int id)
        {
            return _context.Participants.FirstOrDefault(p => p.Id == id);
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post(string Name, string EMial, string Password)
        {
            Participant p = new Participant();
            p.Name = Name; p.EMail = EMial; p.Password = Password;
            _context.Participants.Add(p);
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, string Name, string EMial, string Password)
        {
            Participant ParticipantUpdate = _context.Participants.FirstOrDefault(o => o.Id == id);
            if (ParticipantUpdate != null)
            {
                ParticipantUpdate.Name = Name; ParticipantUpdate.EMail = EMial; ParticipantUpdate.Password = Password;
                _context.Update(ParticipantUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Participant ParticipantRemove = _context.Participants.FirstOrDefault(o => o.Id == id);
            _context.Participants.Remove(ParticipantRemove);
            _context.SaveChanges();
        }
    }
}
