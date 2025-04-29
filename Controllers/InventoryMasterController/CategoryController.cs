using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<string> GenerateNextCatId()
        {
            var lastCategory = await _context.CategoryMasts.OrderByDescending(c => c.CatId).FirstOrDefaultAsync();
            if (lastCategory == null || string.IsNullOrEmpty(lastCategory.CatId))
            {
                return "CT0001"; // If no category exists
            }

            string lastCatId = lastCategory.CatId;
            if (lastCatId.StartsWith("CT") && int.TryParse(lastCatId.Substring(2), out int num))
            {
                return $"CT{(num + 1).ToString("D4")}";
            }
            return "CT0001"; // Fallback
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryMast category)
        {
            if (category == null)
            {
                return BadRequest("Invalid category detail");
            }

            category.CatId = await GenerateNextCatId();
            category.EntryDate = DateTime.UtcNow;

            bool catExists = await _context.CategoryMasts.AnyAsync(c => c.CatId == category.CatId);
            if (catExists)
            {
                return Conflict("Category name already existed");
            }

            _context.CategoryMasts.Add(category);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(decimal id, [FromBody] CategoryMast category)
        {
            if (category == null || id != category.TransID)
            {
                return BadRequest("Invalid data");
            }

            var existingCategory = await _context.CategoryMasts.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not existed");

            }

            bool catExists = await _context.CategoryMasts.AnyAsync(c => c.CatId == category.CatId && c.TransID != id);
            if (catExists)
            {
                return Conflict("Category name already exists");
            }

            existingCategory.CatName = category.CatName;
            existingCategory.VatRate = category.VatRate;
            existingCategory.ModifyBy = category.ModifyBy;
            existingCategory.ModifyDate = DateTime.UtcNow;

            _context.CategoryMasts.Update(existingCategory);
            await _context.SaveChangesAsync();
            return Ok(existingCategory);
        }
    }
}
