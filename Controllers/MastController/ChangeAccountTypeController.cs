using backend.Data;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.MastController
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChangeAccountTypeController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChangeAccountTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("updateAccountType")]

        public async Task<IActionResult> UpdateAccountType([FromBody] ChangeAccountTypeDto changeAccountType )
        {

            if (string.IsNullOrEmpty(changeAccountType.Acc) || string.IsNullOrEmpty(changeAccountType.Acn))
            {
                return BadRequest("Group Code or Group name required");
            }

            var accRecord = await _context.Accs.FirstOrDefaultAsync(a => a.Acc1 == changeAccountType.Acc && a.Acn == changeAccountType.Acn);
            if (accRecord != null)
            {
                return NotFound("Account with provided group code and account not found");
            }

            accRecord.Acc1 = changeAccountType.Acc;

            await _context.SaveChangesAsync();

            return Ok(accRecord);


        }
    }
}
