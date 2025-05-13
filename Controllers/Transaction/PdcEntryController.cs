using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdcEntryController :ControllerBase
    {
        private readonly IPdcEntryServices _services;

        public PdcEntryController(IPdcEntryServices services)
        {
            _services = services;
        }

        [HttpPost("createPDC")]
        public async Task<IActionResult> CreatePDC([FromBody] PDCEntryDto pDCEntry)
        {
            try
            {
                var result = await _services.CreatePDCEntryAsync(pDCEntry);
                return Ok(new { message = "PDC Entry saved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getParties")]
        public async Task<IActionResult> GetCombinedPartyList()
        {
            try
            {
                var partyList = await _services.GetCombinedPartyListAsync();
                return Ok(partyList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getNextTransactionNo")]
        public async Task<IActionResult> GetNextTransactionNo()
        {
            try
            {
                var nextNo = await _services.GetNextTransactionNoAsync();
                return Ok(nextNo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to generate transaction number: {ex.Message}");
            }
        }

    }
}
