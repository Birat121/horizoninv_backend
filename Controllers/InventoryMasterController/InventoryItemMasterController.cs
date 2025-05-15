using backend.Data;
using backend.DTOs;
using backend.Services.InventoryMasterServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemMasterController : ControllerBase
    {
        private readonly IInventoryItemMasterServices _inventoryItemMasterServices;
        private readonly ApplicationDbContext _context;

        public InventoryItemMasterController(IInventoryItemMasterServices inventoryItemMasterServices, ApplicationDbContext context)
        {
            _inventoryItemMasterServices = inventoryItemMasterServices;
            _context = context;
        }

        [HttpPost("createProduct")]
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

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                // Fetch only CatName from the database
                var categoryNames = await _context.CategoryMasts
                    .Select(c => c.CatName)
                    .ToListAsync();

                if (categoryNames == null || !categoryNames.Any())
                {
                    return NotFound(new { message = "No categories found." });
                }

                return Ok(categoryNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching categories.", error = ex.Message });
            }
        }



        [HttpGet("GetSubCategories")]
        public async Task<IActionResult> GetSubCategories([FromQuery] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest(new { message = "Category name is required." });
            }

            var cat = await _context.CategoryMasts
                .FirstOrDefaultAsync(c => c.CatName.ToLower() == categoryName.Trim().ToLower());

            if (cat == null)
            {
                return NotFound(new { message = $"Category '{categoryName}' not found." });
            }

            var subCategoryNames = await _context.SubCategoryMasts
                .Where(sc => sc.CatId == cat.CatId)
                .Select(sc => sc.SubCatName)
                .ToListAsync();

            if (!subCategoryNames.Any())
            {
                return NotFound(new { message = $"No subcategories found for category '{categoryName}'." });
            }

            return Ok(subCategoryNames);
        }

        [HttpGet("GetUOM")]

        public async Task<IActionResult> GetUOM()
        {
            try
            {
                var UOM = await _context.UomMasts
                    .Select(u => u.UomDesc)
                    .ToListAsync();
                if (UOM == null || !UOM.Any())
                {
                    return NotFound(new { message = "UOM not found" });
                }

                return Ok(UOM);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching categories.", error = ex.Message });
            }
        }

        [HttpGet("GetUOMQty")]
        public async Task<IActionResult> GetUOMQty([FromQuery] string uom)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uom))
                    return BadRequest(new { message = "UOM is required." });

                var uomEntry = await _context.UomMasts
                    .Where(x => x.UomDesc == uom)
                    .Select(x => new { x.UomDesc, x.UomQty })
                    .FirstOrDefaultAsync();

                if (uomEntry == null)
                    return NotFound(new { message = $"No quantity found for UOM: {uom}" });

                return Ok(uomEntry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching UOM quantity.", error = ex.Message });
            }
        }


    }
}

