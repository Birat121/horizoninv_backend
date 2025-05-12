using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class BgEntryController : ControllerBase
    {
        private readonly IBgEntryServices _services;

        public BgEntryController(IBgEntryServices services)
        {
            _services = services;
        }

        [HttpPost("createBg")]

        public async Task<IActionResult> CreateBGEntry([FromBody] BgEntryDto entry)
        {
            try
            {
                var createdBgEntry = await _services.CreateEntryAsync(entry);
                return Ok(createdBgEntry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
