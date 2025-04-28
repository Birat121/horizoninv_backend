using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.SystemSecurityController
{

    [Route("api/[controller]")]
    [ApiController]
    public class EndOfDayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EndOfDayController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("createEOD")]
        public async Task<IActionResult> CreateEOD([FromBody] EndOfDay endOfDay)
        {

            if (endOfDay == null)
            {
                return BadRequest("Invalid data");
            }

            _context.EndOfDays.Add(endOfDay);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
