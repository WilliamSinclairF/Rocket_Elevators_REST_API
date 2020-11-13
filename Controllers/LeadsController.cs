using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_REST_API.Models;

namespace Rocket_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly App _context;

        public LeadsController(App context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leads>>> GetLeads()
        {
            return await _context.Leads.ToListAsync();
        }

        // GET: api/Leads/oustanding
        [HttpGet ("outstanding")]
        public async Task<ActionResult<IEnumerable<Leads>>> GetOutstandingLeads()
        {
            return await _context.Leads.Where(l => !_context.Users.Any(u => u.Id == l.UserId) && l.CreatedAt >= DateTime.UtcNow.AddDays(-30)).ToListAsync();
        }

        // GET: api/Leads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leads>> GetLeads(long id)
        {
            var leads = await _context.Leads.FindAsync(id);

            if (leads == null)
            {
                return NotFound();
            }

            return leads;
        }

        // PUT: api/Leads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeads(long id, Leads leads)
        {
            if (id != leads.Id)
            {
                return BadRequest();
            }

            _context.Entry(leads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Leads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Leads>> PostLeads(Leads leads)
        {
            _context.Leads.Add(leads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeads", new { id = leads.Id }, leads);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Leads>> DeleteLeads(long id)
        {
            var leads = await _context.Leads.FindAsync(id);
            if (leads == null)
            {
                return NotFound();
            }

            _context.Leads.Remove(leads);
            await _context.SaveChangesAsync();

            return leads;
        }

        private bool LeadsExists(long id)
        {
            return _context.Leads.Any(e => e.Id == id);
        }
    }
}
