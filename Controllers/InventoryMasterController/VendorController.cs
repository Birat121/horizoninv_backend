using backend.Models;
using backend.Services.InventoryMasterServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers.InventoryMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost("createVendor")]
        public async Task<IActionResult> CreateVendor([FromBody] VendorMast vendor)
        {
            if (vendor == null)
                return BadRequest("Invalid data.");

            var createdVendor = await _vendorService.CreateVendorAsync(vendor);
            return CreatedAtAction(nameof(CreateVendor), new { id = createdVendor.TransID }, createdVendor);
        }

        [HttpPut("updateVendor")]
        public async Task<IActionResult> UpdateVendor([FromBody] VendorMast vendor)
        {
            if (vendor == null || vendor.TransID <= 0)
                return BadRequest("Invalid data.");

            var updatedVendor = await _vendorService.UpdateVendorAsync(vendor);
            if (updatedVendor == null)
                return NotFound("Vendor not found.");

            return Ok(updatedVendor);
        }
    }
}
