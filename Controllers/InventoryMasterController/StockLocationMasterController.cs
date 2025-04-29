using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockLocationMasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockLocationMasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateStockLoc")]
        public async Task<IActionResult> CreateStockLocation([FromBody] StkLoc stockloc)
        {
            if (stockloc == null)
            {
                return BadRequest("Not found");
            }

            // Generate LocId if it's not provided
            if (string.IsNullOrEmpty(stockloc.LocId))
            {
                stockloc.LocId = await GenerateNextLocId();
            }

            bool duplicate = await _context.StkLocs.AnyAsync(st => st.BrNo == stockloc.BrNo);
            if (duplicate)
            {
                return BadRequest("Already existed");
            }

            stockloc.EntDate = DateTime.UtcNow;

            _context.StkLocs.Add(stockloc);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("UpdateStockLoc/{id}")]
        public async Task<IActionResult> UpdateStockLocation([FromBody] StkLoc updatestkLoc, decimal id)
        {
            if (updatestkLoc == null || id != updatestkLoc.TransID)
            {
                return BadRequest("Invalid data");
            }

            var existingLoc = await _context.StkLocs.FindAsync(id);

            if (existingLoc == null)
            {
                return BadRequest("Not existed");
            }

            bool duplicateStock = await _context.StkLocs.AnyAsync(st => st.BrNo == updatestkLoc.BrNo && id != st.TransID);

            if (duplicateStock)
            {
                return Conflict("Item already existed");
            }

            existingLoc.LocName = updatestkLoc.LocName;
            existingLoc.LocId = updatestkLoc.LocId;  // If LocId is part of update, otherwise you can exclude this.

            _context.StkLocs.Update(existingLoc);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Helper method to generate the next LocId in the format "StkL00001"
        private async Task<string> GenerateNextLocId()
        {
            var lastLoc = await _context.StkLocs
                .OrderByDescending(s => s.LocId)
                .FirstOrDefaultAsync();

            int newId = 1;
            if (lastLoc != null)
            {
                var lastIdPart = lastLoc.LocId.Substring(4); // Extract the numeric part after "StkL"
                if (int.TryParse(lastIdPart, out int lastNum))
                {
                    newId = lastNum + 1;
                }
            }

            return $"StkL{newId:D5}"; // Format as StkL00001, where D5 ensures 5 digits with leading zeros
        }
    }
}

