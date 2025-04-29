using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers.MastController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterSettingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CounterSettingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("createCount")]
        public async Task<IActionResult> SaveCounter([FromBody] CountSet countSet)
        {
            if (countSet == null || string.IsNullOrEmpty(countSet.MacID) || string.IsNullOrEmpty(countSet.PCName))
            {
                return BadRequest("MacID and PCName are required.");
            }

            if (_context.CountSets.Any(cs => cs.CountNo == countSet.CountNo))
            {
                return Conflict("CountNo must be Unique.");
            }

            if(_context.CountSets.Any(cs => cs.PCName == countSet.PCName))
            {
                return Conflict("PCName must be unique");
            }

            countSet.SettingDate = DateTime.UtcNow;

            _context.CountSets.Add(countSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SaveCounter), new { id = countSet.TransID }, countSet);
        }

         


    }
}
