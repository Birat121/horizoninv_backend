using backend.Data;
using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalVoucherController : ControllerBase
    {
        private readonly IJournalVoucherService _journalVoucherService;
        private readonly ApplicationDbContext _context;

        public JournalVoucherController(IJournalVoucherService journalVoucherService, ApplicationDbContext context)
        {
            _journalVoucherService = journalVoucherService;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] JVNEntryDto entryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _journalVoucherService.CreateJournalEntryAsync(entryDto);
                return Ok(new { message = "Journal entry created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet("JournalNo")]
        public async Task<IActionResult> GetNextVoucherNo()
        {
            var lastVoucher = await _context.JVNs
                .OrderByDescending(jv => jv.VoucherRef)
                .Select(jv => jv.VoucherRef)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastVoucher) && lastVoucher.StartsWith("JV81-"))
            {
                string numberPart = lastVoucher.Substring(5);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            string newVoucherNo = $"JV81-{nextNumber.ToString("D7")}";
            return Ok(new { voucherNo = newVoucherNo });
        }

        [HttpGet("AccountName")]
        public async Task<IActionResult> GetAccountNames()
        {
            var names = await _context.Accs
                .Where(a => a.AclD == 1)               // ✅ Filter by Acl1 = 1
                .Select(a => a.Acn)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();

            return Ok(names);
        }

    }
}

