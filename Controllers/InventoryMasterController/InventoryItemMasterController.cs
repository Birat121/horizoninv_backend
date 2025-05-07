using backend.DTOs;
using backend.Services.InventoryMasterServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.InventoryMasterController
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemMasterController : ControllerBase
    {
        private readonly IInventoryItemMasterServices _inventoryItemMasterServices;

        public InventoryItemMasterController(IInventoryItemMasterServices inventoryItemMasterServices)
        {
            _inventoryItemMasterServices = inventoryItemMasterServices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            if (product == null || product.ProductMastDTO == null || product.productUOMDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var result = await _inventoryItemMasterServices.AddProductAsync(product);
                if (result)
                    return Ok("Product created successfully.");
                else
                    return StatusCode(500, "Failed to create product.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}

