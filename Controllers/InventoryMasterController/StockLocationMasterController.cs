using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{

    [Route("api/controller")]
    [ApiController]
    public class StockLocationMasterController :ControllerBase
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
                return BadRequest("not existed");
            }

            bool duplicateStock = await _context.StkLocs.AnyAsync(st => st.BrNo == updatestkLoc.BrNo && id != st.TransID);

            if (duplicateStock)
            {
                return Conflict("Item already existed");
            }


            existingLoc.LocName = updatestkLoc.LocName;
            existingLoc.LocId = updatestkLoc.LocId;

            _context.StkLocs.Update(existingLoc);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
