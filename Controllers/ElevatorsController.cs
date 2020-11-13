using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_REST_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocket_REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly App _context;

        public ElevatorsController(App context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevators>>> GetElevators()
        {
            return await _context.Elevators.ToListAsync();
        }

        // GET: api/Elevators/oos
        [HttpGet("oos")]
        public async Task<ActionResult<IEnumerable<Elevators>>> GetOOSElevators()
        {
            var outOfServiceElevators = await _context.Elevators
        .Where(e => !e.ElevatorStatus.Contains("ACTIVE"))
        .ToListAsync();
            return outOfServiceElevators;
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> GetElevators(long id)
        {
            var elevators = await _context.Elevators.FindAsync(id);

            if (elevators == null)
            {
                return NotFound();
            }

            return elevators;
        }

        // Get the status of an elevator
        [HttpGet("status/{id}")]
        public async Task<ActionResult<ElevatorDTO>> GetElevatorStatus(long id)
        {
            var elevator = await _context.Elevators.Select(e =>
            new ElevatorDTO()
            {
                Id = e.Id,
                ElevatorStatus = e.ElevatorStatus
            })
            .SingleOrDefaultAsync(e => e.Id == id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        // Change the status of an elevator
        // PUT: api/Elevators/status/5
        // request body in json must include "Id" and "ElevatorStatus"
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}")]
        public async Task<IActionResult> ChangeElevatorStatus(long id, ElevatorDTO elevatorDTO)
        {
            if (id != elevatorDTO.Id)
            {
                return BadRequest();
            }

            var elevator = await _context.Elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            elevator.ElevatorStatus = elevatorDTO.ElevatorStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorsExists(id))
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

        // PUT: api/Elevators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevators(long id, Elevators elevators)
        {
            if (id != elevators.Id)
            {
                return BadRequest();
            }

            _context.Entry(elevators).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorsExists(id))
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

        // POST: api/Elevators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Elevators>> PostElevators(Elevators elevators)
        {
            _context.Elevators.Add(elevators);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevators", new { id = elevators.Id }, elevators);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Elevators>> DeleteElevators(long id)
        {
            var elevators = await _context.Elevators.FindAsync(id);
            if (elevators == null)
            {
                return NotFound();
            }

            _context.Elevators.Remove(elevators);
            await _context.SaveChangesAsync();

            return elevators;
        }

        private bool ElevatorsExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }
    }
}
