using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class IInvoiceGenerateController : ControllerBase
    {
        private readonly IInvoiceGenerateService _invoiceGenerateService;

        public IInvoiceGenerateController(IInvoiceGenerateService invoiceGenerateService)
        {
            _invoiceGenerateService = invoiceGenerateService;
        }

        [HttpPost("SaveInvoice")]

        public async Task<IActionResult> Create([FromBody] CreateInvGDto dto)
        {
            try
            {
                await _invoiceGenerateService.CreateInvgAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
