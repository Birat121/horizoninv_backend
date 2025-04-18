using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/controller")]
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
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
