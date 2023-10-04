﻿using FestFindV2.Models;
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
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _context.Events.ToList();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Event Get(int id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post(string Name, string Description, string Category, string Location, DateTime Date_Time, float Costs, int MaxParticipants)
        {
            Event e = new Event();
            e.Name = Name; e.Description = Description; e.Category = Category; e.Location = Location; e.Date_Time = Date_Time; e.Costs = Costs; e.MaxParticipants = MaxParticipants;
            _context.Events.Add(e); 
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, string Name, string Description, string Category, string Location, DateTime Date_Time, float Costs, int MaxParticipants)
        {
            Event EventUpdate = _context.Events.FirstOrDefault(e => e.Id == id);
            if (EventUpdate != null)
            {
                EventUpdate.Name = Name; EventUpdate.Description = Description; EventUpdate.Category = Category; EventUpdate.Location = Location; EventUpdate.Date_Time = Date_Time; EventUpdate.Costs = Costs; EventUpdate.MaxParticipants = MaxParticipants;
                _context.Update(EventUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Event EventRemove = _context.Events.FirstOrDefault(e => e.Id == id);
            _context.Events.Remove(EventRemove); 
            _context.SaveChanges();
        }
    }
}