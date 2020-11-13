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
    public class BatteriesController : ControllerBase
    {
        private readonly App _context;

        public BatteriesController(App context)
        {
            _context = context;
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batteries>>> GetBatteries()
        {
            return await _context.Batteries.ToListAsync();
        }

        // GET: api/Batteries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Batteries>> GetBatteries(long id)
        {
            var batteries = await _context.Batteries.FindAsync(id);

            if (batteries == null)
            {
                return NotFound();
            }

            return batteries;
        }


        // Get the status of a battery
        [HttpGet("status/{id}")]
        public async Task<ActionResult<BatteryDTO>> GetBatteryStatus(long id)
        {
            var battery = await _context.Batteries.Select(b =>
            new BatteryDTO()
            {
                Id = b.Id,
                BatteryStatus = b.BatteryStatus
            })
            .SingleOrDefaultAsync(b => b.Id == id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        // Change the status of a battery
        // PUT: api/Batteries/status/5
        // request body in json must include "Id" and "BatteryStatus"
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}")]
        public async Task<IActionResult> ChangeBatteryStatus(long id, BatteryDTO batteryDTO)
        {
            if (id != batteryDTO.Id)
            {
                return BadRequest();
            }

            var battery = await _context.Batteries.FindAsync(id);
            if (battery == null)
            {
                return NotFound();
            }

            battery.BatteryStatus = batteryDTO.BatteryStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteriesExists(id))
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

        // PUT: api/Batteries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatteries(long id, Batteries batteries)
        {
            if (id != batteries.Id)
            {
                return BadRequest();
            }

            _context.Entry(batteries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteriesExists(id))
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

        // POST: api/Batteries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Batteries>> PostBatteries(Batteries batteries)
        {
            _context.Batteries.Add(batteries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatteries", new { id = batteries.Id }, batteries);
        }

        // DELETE: api/Batteries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Batteries>> DeleteBatteries(long id)
        {
            var batteries = await _context.Batteries.FindAsync(id);
            if (batteries == null)
            {
                return NotFound();
            }

            _context.Batteries.Remove(batteries);
            await _context.SaveChangesAsync();

            return batteries;
        }

        private bool BatteriesExists(long id)
        {
            return _context.Batteries.Any(e => e.Id == id);
        }
    }
}
