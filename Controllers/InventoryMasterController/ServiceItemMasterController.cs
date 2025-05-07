using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers.InventoryMasterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemMasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceItemMasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to generate Itemcode
        private async Task<string> GenerateNextItemCode()
        {
            var lastItem = await _context.Service_Is.OrderByDescending(s => s.Itemcode).FirstOrDefaultAsync();
            if (lastItem == null || string.IsNullOrEmpty(lastItem.Itemcode))
            {
                return "SI0001"; // If no item exists, start from SI0001
            }

            string lastItemCode = lastItem.Itemcode;
            if (lastItemCode.StartsWith("SI") && int.TryParse(lastItemCode.Substring(2), out int num))
            {
                return $"SI{(num + 1).ToString("D4")}";
            }
            return "SI0001"; // Fallback
        }

        // POST: api/ServiceItemMaster/CreateServiceItem
        [HttpPost("CreateServiceItem")]
        public async Task<IActionResult> CreateServiceItem([FromBody] Service_I serviceItem)
        {
            if (serviceItem == null)
            {
                return BadRequest("Invalid service item data");
            }

            // Check if Itemcode is provided, if not, generate a new Itemcode
            if (string.IsNullOrEmpty(serviceItem.Itemcode))
            {
                serviceItem.Itemcode = await GenerateNextItemCode(); // Generate the next Itemcode
            }

            // Set default values for EnteredBy and EnteredSys if not provided
            if (string.IsNullOrEmpty(serviceItem.EnteredBy))
            {
                serviceItem.EnteredBy = "System";  // Default to "System"
            }

            if (string.IsNullOrEmpty(serviceItem.EnteredSys))
            {
                serviceItem.EnteredSys = "Backend";  // Default to "Backend"
            }

            serviceItem.EnteredDate = DateTime.UtcNow;  // Set EnteredDate

            // Manually clear validation errors
            ModelState.ClearValidationState(nameof(serviceItem));

            // Save to the database
            bool itemExists = await _context.Service_Is.AnyAsync(s => s.Itemcode == serviceItem.Itemcode);
            if (itemExists)
            {
                return Conflict("Service item already exists.");
            }

            _context.Service_Is.Add(serviceItem);
            await _context.SaveChangesAsync();

            return Ok(serviceItem);
        }


        // PUT: api/ServiceItemMaster/UpdateServiceItem/{id}
        [HttpPut("UpdateServiceItem/{id}")]
        public async Task<IActionResult> UpdateServiceItem([FromBody] Service_I updateService, decimal id)
        {
            if (updateService == null || id != updateService.TransID)
            {
                return BadRequest("Invalid data");
            }

            var existingService = await _context.Service_Is.FindAsync(id);

            if (existingService == null)
            {
                return BadRequest("Service item not found");
            }

            bool duplicateItem = await _context.Service_Is.AnyAsync(s => s.Itemcode == updateService.Itemcode && s.TransID != id);
            if (duplicateItem)
            {
                return Conflict("Itemcode already exists.");
            }

            existingService.Description = updateService.Description;
            existingService.Rt = updateService.Rt;

            // Ensure EnteredBy and EnteredSys are updated, if provided
            existingService.EnteredBy = updateService.EnteredBy ?? existingService.EnteredBy;
            existingService.EnteredSys = updateService.EnteredSys ?? existingService.EnteredSys;
            

            _context.Service_Is.Update(existingService);
            await _context.SaveChangesAsync();

            return Ok(existingService); 
        }
    }
}
