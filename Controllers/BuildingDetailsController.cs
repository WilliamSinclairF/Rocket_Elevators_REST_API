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
    public class BuildingDetailsController : ControllerBase
    {
        private readonly App _context;

        public BuildingDetailsController(App context)
        {
            _context = context;
        }

        // GET: api/BuildingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDetails>>> GetBuildingDetails()
        {
            return await _context.BuildingDetails.ToListAsync();
        }

        // GET: api/BuildingDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDetails>> GetBuildingDetails(long id)
        {
            var buildingDetails = await _context.BuildingDetails.FindAsync(id);

            if (buildingDetails == null)
            {
                return NotFound();
            }

            return buildingDetails;
        }

        // PUT: api/BuildingDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildingDetails(long id, BuildingDetails buildingDetails)
        {
            if (id != buildingDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(buildingDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingDetailsExists(id))
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

        // POST: api/BuildingDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BuildingDetails>> PostBuildingDetails(BuildingDetails buildingDetails)
        {
            _context.BuildingDetails.Add(buildingDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildingDetails", new { id = buildingDetails.Id }, buildingDetails);
        }

        // DELETE: api/BuildingDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BuildingDetails>> DeleteBuildingDetails(long id)
        {
            var buildingDetails = await _context.BuildingDetails.FindAsync(id);
            if (buildingDetails == null)
            {
                return NotFound();
            }

            _context.BuildingDetails.Remove(buildingDetails);
            await _context.SaveChangesAsync();

            return buildingDetails;
        }

        private bool BuildingDetailsExists(long id)
        {
            return _context.BuildingDetails.Any(e => e.Id == id);
        }
    }
}
