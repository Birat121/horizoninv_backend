using backend.Models;
using backend.Services.InventoryMasterServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.InventoryMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost("createCustomer")]

        public async Task<IActionResult> CreateCustomer([FromBody] CustomerMast customer)
        {
            if (customer == null)
            {
                return NotFound();
            }

            var CreatedCustomer = await _customerServices.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(CreateCustomer), new { id = CreatedCustomer.TransID }, CreatedCustomer);

        }

        [HttpPut("updateCustomer")]

        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerMast updateCustomer)
        {
            if (updateCustomer == null || updateCustomer.TransID <= 0)
                return BadRequest("Invalid data.");

            var updatedCustomer = await _customerServices.UpdateCustomerAsync(updateCustomer);
            if (updatedCustomer == null)
                return NotFound("Vendor not found.");

            return Ok(updatedCustomer);
        }
    }
}