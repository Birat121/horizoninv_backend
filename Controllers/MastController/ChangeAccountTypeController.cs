using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.MastController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeAccountTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChangeAccountTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("updateAccountType")]
        public async Task<IActionResult> UpdateAccountType([FromBody] ChangeAccountTypeDto changeAccountType)
        {
            // Validation for required fields
            if (string.IsNullOrEmpty(changeAccountType.Acc) || string.IsNullOrEmpty(changeAccountType.Acn))
            {
                return BadRequest("Group Code or Group name required");
            }

            // Find the account record based on provided Acc and Acn
            var accRecord = await _context.Accs.FirstOrDefaultAsync(a => a.Acc1 == changeAccountType.Acc && a.Acn == changeAccountType.Acn);

            // If the account record is not found, return NotFound response
            if (accRecord == null)
            {
                return NotFound("Account with provided group code and account not found");
            }

            // Update the account type (AccType or similar field)
            accRecord.AcType = changeAccountType.Actype;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated account record
            return Ok(accRecord);
        }
    }
}

