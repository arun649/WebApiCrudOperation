using DemoWebApi.Data;
using DemoWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace DemoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        private readonly ApplicationContext _context;

        public RegistrationController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Registration
        [HttpGet]
        public async Task<IActionResult> GetAllRegistrations()
        {
            var list = await _context.registrations.ToListAsync();
            return Ok(list);
        }

        // GET: api/Registration/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRegistrationById(int id)
        {
            var registration = await _context.registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return Ok(registration);
        }

        // POST: api/Registration
        [HttpPost]
        public async Task<IActionResult> AddRegistration([FromBody] Registration registration)
        {
            if (registration == null || registration.Id != 0)
            {
                return BadRequest("Invalid registration data.");
            }

            await _context.registrations.AddAsync(registration);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRegistrationById), new { id = registration.Id }, registration);
        }

        // PUT: api/Registration/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRegistration([FromBody] Registration registration, int id)
        {
            var existingRegistration = await _context.registrations.FindAsync(id);
            if (existingRegistration == null)
            {
                return NotFound();
            }

            existingRegistration.Name = registration.Name;
            existingRegistration.Email = registration.Email;
            existingRegistration.Description = registration.Description;
            existingRegistration.EmailAddress = registration.EmailAddress;
            existingRegistration.PhoneNumber = registration.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(existingRegistration);
        }

        // DELETE: api/Registration/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _context.registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return Ok(registration);
        }
    }
}
