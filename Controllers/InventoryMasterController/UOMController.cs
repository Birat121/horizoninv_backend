using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{

    [Route("api/controllers")]
    [ApiController]
    public class UOMController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UOMController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateUom")]

        public async Task<IActionResult> CreateUOM([FromBody] UomMast uom)
        {
            if (uom == null)
            {
                return BadRequest("Invalid UOM data");
            }

            bool uomExists = await _context.UomMasts.AnyAsync(u => u.UomDesc == uom.UomDesc);
            if (uomExists)
            {
                return Conflict("Uom description already exists");
            }

            uom.EntryDate = DateTime.UtcNow;

            _context.UomMasts.Add(uom);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateUOM), new { id = uom.TransID }, uom);

        }

        [HttpPut("UpdateUom/{id}")]
        public async Task<IActionResult> UpdateUOM(decimal id, [FromBody] UomMast uom)
        {
            if(uom == null || id != uom.TransID)
            {
                return BadRequest("Invalid Data");
            }

            var existingUom = await _context.UomMasts.FindAsync(id);
            if(existingUom == null)
            {
                return NotFound("Not found");
            }

            bool uomExists = await _context.UomMasts.AnyAsync(u => u.UomDesc == uom.UomDesc && u.TransID != id);
            if (uomExists)
            {
                return Conflict("Already exists");
            }

            existingUom.UomDesc = uom.UomDesc;
            existingUom.UomQty = uom.UomQty;

            _context.UomMasts.Update(existingUom);
            await _context.SaveChangesAsync();

            return Ok(existingUom);

        }
    }
}
