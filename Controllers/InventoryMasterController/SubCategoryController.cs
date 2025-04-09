using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{

    [Route("api/controllers")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("createSubCategory")]

        public async Task<IActionResult> CreateSubCategoryAsync([FromBody] SubCategoryMast subCategory)
        {
            if (subCategory == null)
            {
                return BadRequest("Invalid data");
            }

            var category = await _context.CategoryMasts.FirstOrDefaultAsync(c => c.CatName == subCategory.CatId);
            if (category == null)
            {
                return BadRequest("Invalid data");
            }

            subCategory.CatId = category.CatId;
            subCategory.EntryDate = DateTime.UtcNow;

            _context.SubCategoryMasts.Add(subCategory);
            await _context.SaveChangesAsync();
            return Ok(subCategory);
        }

        [HttpPut("updateSubCategory/{id}")]

        public async Task<IActionResult> UpdateSubCategoryAsync([FromBody] SubCategoryMast updateSubCategory,decimal id)
        {
            var existingSubCategory = await _context.SubCategoryMasts.FindAsync(id);

            if (existingSubCategory == null)
            {
                return NotFound();
            }

            var category = await _context.CategoryMasts.FirstOrDefaultAsync(c => c.CatName == updateSubCategory.CatId);

            if (category == null)
            {
                return BadRequest("Category Not found");
            }

            existingSubCategory.SubCatName = updateSubCategory.SubCatName;

            _context.SubCategoryMasts.Update(existingSubCategory);
            await _context.SaveChangesAsync();
            return Ok(existingSubCategory);
        }


    }
}
