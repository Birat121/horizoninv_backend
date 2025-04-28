using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{

    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeLabelPrintController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BarcodeLabelPrintController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("BarcodePrint")]

        public async Task<IActionResult> CreateBarcode([FromBody] BarPR bar)
        {
            if (bar == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                bar.PDate = DateTime.UtcNow;
                await _context.BarPRs.AddAsync(bar);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
