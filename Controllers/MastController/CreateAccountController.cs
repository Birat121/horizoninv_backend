using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.MastController
{

    [ApiController]
    [Route("api/[controller]")]
    public class CreateAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreateAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("createAcc")]

        public async Task<IActionResult> CreateAccount([FromBody] ChangeAccountTypeDto accDto)
        {
            if (string.IsNullOrEmpty(accDto.Acc) || string.IsNullOrEmpty(accDto.Acn) || string.IsNullOrEmpty(accDto.Actype))
            {
                return BadRequest("Acc, Acn, and AcType are required.");
            }

            var existingAccount = await _context.Accs.FirstOrDefaultAsync(a => a.Acc1 == accDto.Acc && a.Acn == accDto.Acn);
            if (existingAccount != null)
            {
                return Conflict("Account already exists with the provided Acc and Acn.");
            }

            var newAccount = new Acc
            {
                Acc1 = accDto.Acc,
                Acn = accDto.Acn,
                AcType = accDto.Actype,
                AclO = 1,
                AclD = 0,
                L0 = accDto.Acc,
                MasterAcType = accDto.Actype,

            };

            _context.Accs.Add(newAccount);
            await _context.SaveChangesAsync();
            return Ok(newAccount);

        }

        [HttpPut("updateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] ChangeAccountTypeDto accountDto)
        {
            // Validate the required fields
            if (string.IsNullOrEmpty(accountDto.Acc) || string.IsNullOrEmpty(accountDto.Acn))
            {
                return BadRequest("Acc and Acn are required.");
            }

            // Find the existing account by Acc and Acn (or by Acc1 and Acn, based on your model)
            var existingAccount = await _context.Accs.FirstOrDefaultAsync(a => a.Acc1 == accountDto.Acc && a.Acn == accountDto.Acn);

            if (existingAccount == null)
            {
                return NotFound("Account not found with the provided Acc and Acn.");
            }

            // Update only the Acn field (the other fields will remain unchanged)
            existingAccount.Acn = accountDto.Acn;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingAccount);
        }

        private async Task<string> GenerateChildAcc1(string parentAcc1)
        {
            var baseCode = parentAcc1 + "-";
            var existingCodes = await _context.Accs
                .Where(a => a.Acc1.StartsWith(baseCode))
                .Select(a => a.Acc1)
                .ToListAsync();

            int maxSuffix = 0;

            foreach (var code in existingCodes)
            {
                var suffixPart = code.Substring(baseCode.Length);
                if (int.TryParse(suffixPart, out int suffix))
                {
                    if (suffix > maxSuffix)
                        maxSuffix = suffix;
                }
            }

            // Increment the largest found suffix
            int newSuffix = maxSuffix + 1;
            return $"{baseCode}{newSuffix.ToString("D3")}"; // Pads with zeros, e.g., -001
        }


        [HttpPost("createSubGroup")]

        public async Task<IActionResult> createSubGroup([FromBody] SubGroupDto subGroup)
        {
            if (string.IsNullOrWhiteSpace(subGroup.ParentGroupName) || string.IsNullOrWhiteSpace(subGroup.AccountType) || string.IsNullOrWhiteSpace(subGroup.SubGroupName))
                return BadRequest("All fields are required.");

            var parent = await _context.Accs.FirstOrDefaultAsync(a => a.Acn == subGroup.ParentGroupName && a.AcType == subGroup.AccountType);

            if (parent == null)
            {
                return NotFound("Parent group account not found");
            }

            var newAcc1 = await GenerateChildAcc1(parent.Acc1);



            var newsubGroup = new Acc
            {
                Acn = subGroup.SubGroupName,
                AcType = parent.AcType,
                MasterAcType = parent.MasterAcType,
                AclO = 0,
                AclD = 1,
                Acg = parent.Acg,
                Acgg = parent.Acgg,
                L0 = parent.Acc1,
                Acc1 = newAcc1,

            };

            _context.Accs.Add(newsubGroup);
            await _context.SaveChangesAsync();
            return Ok(newsubGroup);

        }


    }
}
