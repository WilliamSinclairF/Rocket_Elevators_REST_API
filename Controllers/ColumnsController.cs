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
    public class ColumnsController : ControllerBase
    {
        private readonly App _context;

        public ColumnsController(App context)
        {
            _context = context;
        }

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> GetColumns()
        {
            return await _context.Columns.ToListAsync();
        }

        // GET: api/Columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> GetColumns(long id)
        {
            var columns = await _context.Columns.FindAsync(id);

            if (columns == null)
            {
                return NotFound();
            }

            return columns;
        }

        // Get the status of a column
        [HttpGet("status/{id}")]
        public async Task<ActionResult<ColumnDTO>> GetColumnStatus(long id)
        {
            var column = await _context.Columns.Select(c =>
            new ColumnDTO()
            {
                Id = c.Id,
                ColumnStatus = c.ColumnStatus
            })
            .SingleOrDefaultAsync(c => c.Id == id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

        // Change the status of a column
        // PUT: api/Columns/status/5
        // request body in json must include "Id" and "ColumnStatus"
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("status/{id}")]
        public async Task<IActionResult> ChangeColumnStatus(long id, ColumnDTO columnDTO)
        {
            if (id != columnDTO.Id)
            {
                return BadRequest();
            }

            var column = await _context.Columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            column.ColumnStatus = columnDTO.ColumnStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnsExists(id))
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

        // PUT: api/Columns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumns(long id, Columns columns)
        {
            if (id != columns.Id)
            {
                return BadRequest();
            }

            _context.Entry(columns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnsExists(id))
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

        // POST: api/Columns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Columns>> PostColumns(Columns columns)
        {
            _context.Columns.Add(columns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumns", new { id = columns.Id }, columns);
        }

        // DELETE: api/Columns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Columns>> DeleteColumns(long id)
        {
            var columns = await _context.Columns.FindAsync(id);
            if (columns == null)
            {
                return NotFound();
            }

            _context.Columns.Remove(columns);
            await _context.SaveChangesAsync();

            return columns;
        }

        private bool ColumnsExists(long id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}
