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

    }
}
