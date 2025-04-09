using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace backend.Controllers.SystemSecurityController
{

    [Route("api/controller")]
    [ApiController]
    public class GreetingController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GreetingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("saveNote")]
        public async Task<IActionResult> SaveGreetingNote([FromBody] GreetingNote greetingNote)
        {
            if (greetingNote == null)
            {
                return BadRequest("Invalid Data");
            }
            var existingNote = await _context.Set<GreetingNote>().FirstOrDefaultAsync(g=>g.TransID == greetingNote.TransID);
            if (existingNote == null)
            {
                greetingNote.TransID = await _context.Set<GreetingNote>().MaxAsync(g => (decimal?)g.TransID) + 1 ?? 1;
                greetingNote.UpdateDate = DateTime.UtcNow;
                _context.Set<GreetingNote>().Add(greetingNote);
            }
            else
            {
                // Update existing Greeting Note
                existingNote.GreetingNote1 = greetingNote.GreetingNote1;
                existingNote.UpdateBy = greetingNote.UpdateBy;
                existingNote.UpdateSys = greetingNote.UpdateSys;
                existingNote.UpdateDate = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
            return Ok(existingNote);


        }


    

    }
}
