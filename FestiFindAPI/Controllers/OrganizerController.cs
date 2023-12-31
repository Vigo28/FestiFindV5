﻿using FestFindV2.Models;
using FestiFindV5.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrganizerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<EventController>
        /// <summary>
        /// Get all organizers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Organizer> Get()
        {
            return _context.Organizers.ToList();
        }

        // GET api/<EventController>/5
        /// <summary>
        /// Get organizer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Organizer Get(int id)
        {
            return _context.Organizers.FirstOrDefault(o => o.Id == id);
        }

        // POST api/<EventController>
        /// <summary>
        /// Create new organizer
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="EMial"></param>
        /// <param name="Password"></param>
        [HttpPost]
        public void Post(string Name, string EMial, string Password)
        {
            Organizer o = new Organizer();
            o.Name = Name; o.EMail = EMial; o.Password = Password;
            _context.Organizers.Add(o);
            _context.SaveChanges();
        }

        // PUT api/<EventController>/5
        /// <summary>
        /// Edit organizer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="EMial"></param>
        /// <param name="Password"></param>
        [HttpPut("{id}")]
        public void Put(int id, string Name, string EMial, string Password)
        {
            Organizer OrganizerUpdate = _context.Organizers.FirstOrDefault(o => o.Id == id);
            if (OrganizerUpdate != null)
            {
                OrganizerUpdate.Name = Name; OrganizerUpdate.EMail = EMial; OrganizerUpdate.Password = Password;
                _context.Update(OrganizerUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<EventController>/5
        /// <summary>
        /// Delete organizer
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Organizer OrganizerRemove = _context.Organizers.FirstOrDefault(o => o.Id == id);
            _context.Organizers.Remove(OrganizerRemove);
            _context.SaveChanges();
        }
    }
}
