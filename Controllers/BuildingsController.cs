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
    public class BuildingsController : ControllerBase
    {
        private readonly App _context;

        public BuildingsController(App context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

        // GET: api/Buildings/interventions
        [HttpGet("interventions")]
        public async Task<IEnumerable<BuildingDTO>> GetBuildingsWithInterventions()
        {
            var buildings = await (from building in _context.Buildings
                                   join battery in _context.Batteries on building.Id equals battery.BuildingId
                                   join column in _context.Columns on battery.Id equals column.BatteryId
                                   join elevator in _context.Elevators on column.Id equals elevator.ColumnId
                                   select new BuildingDTO
                                   {
                                       BuildingId = building.Id,
                                       Batteries = (List<Batteries>)_context.Batteries
                                   .Where(b => b.BuildingId == building.Id && b.BatteryStatus != "ACTIVE"),
                                       Columns = (List<Columns>)_context.Columns
                                   .Where(c => c.BatteryId == battery.Id && c.ColumnStatus != "ACTIVE"),
                                       Elevators = (List<Elevators>)_context.Elevators
                                   .Where(e => e.ColumnId == column.Id && e.ElevatorStatus != "ACTIVE"),
                                   })
                                   .Distinct()
                                   .ToListAsync();

            foreach (BuildingDTO b in buildings.ToList())
            {
                var countTo3 = 0;
                if (!b.Batteries.Any()) {
                    countTo3++;
                }
                if (!b.Columns.Any())
                {
                    countTo3++;
                }
                if (!b.Elevators.Any())
                {
                    countTo3++;
                }
                if (countTo3 == 3)
                {
                    buildings.Remove(b);
                }
            }

            return buildings;

        }


        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(long id)
        {
            var buildings = await _context.Buildings.FindAsync(id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildings(long id, Buildings buildings)
        {
            if (id != buildings.Id)
            {
                return BadRequest();
            }

            _context.Entry(buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingsExists(id))
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

        // POST: api/Buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Buildings>> PostBuildings(Buildings buildings)
        {
            _context.Buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = buildings.Id }, buildings);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buildings>> DeleteBuildings(long id)
        {
            var buildings = await _context.Buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(buildings);
            await _context.SaveChangesAsync();

            return buildings;
        }

        private bool BuildingsExists(long id)
        {
            return _context.Buildings.Any(e => e.Id == id);
        }
    }
}
