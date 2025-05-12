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

        // Fetch all Group Names
        [HttpGet("getGroupNames")]
        public async Task<IActionResult> GetGroupNames()
        {
            var groupNames = await _context.Accs
                .Where(a => a.AclD == 1) // Filter based on Acl1 = 1
                .Select(a => a.Acn) // Assuming Acn is the group name
                .Distinct()
                .ToListAsync();

            return Ok(groupNames);
        }

        [HttpGet("getGroupCodeByName/{groupName}")]
        public async Task<IActionResult> GetGroupCodeByName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return BadRequest("Group name is required");

            var group = await _context.Accs
                .Where(a => a.Acn == groupName)
                .Select(a => new { GroupCode = a.Acc1 }) // or your actual group code field
                .FirstOrDefaultAsync();

            if (group == null)
                return NotFound("Group not found");

            return Ok(group);
        }


        [HttpGet("getAccountTypes")]
        public async Task<IActionResult> GetAccountTypes()
        {
            try
            {
                // Fetch all account types from the database
                var accountTypes = await _context.Accs
                    .Select(a => a.AcType)
                    .Distinct()
                    .ToListAsync();

                return Ok(accountTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving account types: " + ex.Message);
            }
        }


        // Update Account Type
        [HttpPut("updateAccountType")]
        public async Task<IActionResult> UpdateAccountType([FromBody] ChangeAccountTypeDto changeAccountType)
        {
            // Validation for required fields
            if (string.IsNullOrEmpty(changeAccountType.Acc) || string.IsNullOrEmpty(changeAccountType.Acn))
            {
                return BadRequest("Group Code or Group Name required");
            }

            // Find the account record based on provided Acc and Acn
            var accRecord = await _context.Accs.FirstOrDefaultAsync(a => a.Acc1 == changeAccountType.Acc && a.Acn == changeAccountType.Acn);

            // If the account record is not found, return NotFound response
            if (accRecord == null)
            {
                return NotFound("Account with provided group code and account name not found");
            }

            // Update the account type (AcType or similar field)
            accRecord.AcType = changeAccountType.Actype;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated account record
            return Ok(accRecord);
        }
    }
}


