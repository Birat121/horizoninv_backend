using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalVoucherController : ControllerBase
    {
        private readonly IJournalVoucherService _journalVoucherService;

        public JournalVoucherController(IJournalVoucherService journalVoucherService)
        {
            _journalVoucherService = journalVoucherService;
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
    }
}

