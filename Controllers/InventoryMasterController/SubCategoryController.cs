﻿using backend.Data;
using backend.Models;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers.InventoryMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to generate SubCatID
        private async Task<string> GenerateNextSubCatId(string catId)
        {
            var prefix = $"{catId}-";

            var existingSuffixes = await _context.SubCategoryMasts
                .Where(sc => sc.SubCatID.StartsWith(prefix))
                .Select(sc => sc.SubCatID.Substring(prefix.Length))
                .ToListAsync();

            int max = existingSuffixes
                .Select(s => int.TryParse(s, out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            var nextSuffix = (max + 1).ToString().PadLeft(3, '0');
            return prefix + nextSuffix;
        }


        // POST: api/SubCategory/createSubCategory
        [HttpPost("createSubCategory")]
        public async Task<IActionResult> CreateSubCategoryAsync([FromBody] SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var category = await _context.CategoryMasts
                .FirstOrDefaultAsync(c => c.CatName == subCategoryDto.CatName);

            if (category == null)
            {
                return BadRequest("Category not found.");
            }

            // Check for existing subcategory to prevent duplicate SubCatID
            var existingSubCategory = await _context.SubCategoryMasts
                .FirstOrDefaultAsync(sc => sc.CatId == category.CatId && sc.SubCatName == subCategoryDto.SubCatName);

            if (existingSubCategory != null)
            {
                return BadRequest("Subcategory already exists.");
            }

            // Generate SubCatID using the new helper method
            var subCatID = await GenerateNextSubCatId(category.CatId);

            var subCategory = new SubCategoryMast
            {
                CatId = category.CatId,
                SubCatID = subCatID,
                SubCatName = subCategoryDto.SubCatName,
                VatRate = category.VatRate, // Assuming VatRate is inherited from the category
                EntryDate = DateTime.UtcNow,
                EntryBy = "System" // Replace with actual user context if available
            };

            _context.SubCategoryMasts.Add(subCategory);
            await _context.SaveChangesAsync();

            return Ok(subCategory);
        }

        // PUT: api/SubCategory/updateSubCategory/{id}
        [HttpPut("updateSubCategory/{id}")]
        public async Task<IActionResult> UpdateSubCategoryAsync(decimal id, [FromBody] SubCategoryDto subCategoryDto)
        {
            if (subCategoryDto == null)
                return BadRequest("Invalid data.");

            var existingSubCategory = await _context.SubCategoryMasts.FindAsync(id);

            if (existingSubCategory == null)
                return NotFound("SubCategory not found.");

            var category = await _context.CategoryMasts
                .FirstOrDefaultAsync(c => c.CatName == subCategoryDto.CatName);

            if (category == null)
                return BadRequest("Category not found.");

            existingSubCategory.CatId = category.CatId;
            existingSubCategory.SubCatName = subCategoryDto.SubCatName;
            existingSubCategory.VatRate = category.VatRate; // Update VatRate if category changed
            

            _context.SubCategoryMasts.Update(existingSubCategory);
            await _context.SaveChangesAsync();

            return Ok(existingSubCategory);
        }
    }
}

