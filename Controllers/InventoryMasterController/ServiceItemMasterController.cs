using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.InventoryMasterController
{

    [Route("api/controller")]
    [ApiController]
    public class ServiceItemMasterController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceItemMasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateServiceItem")]

        public async Task<IActionResult> CreateServiceItem([FromBody] Service_I serviceItem)
        {
            if (serviceItem == null)
            {
                return BadRequest("Invalid service item data");
            }

            bool itemExists = await _context.Service_Is.AnyAsync(s => s.Itemcode == serviceItem.Itemcode);
            if (itemExists)
            {
                return Conflict("Already existed");
            }

            serviceItem.EnteredDate = DateTime.UtcNow;

            _context.Service_Is.Add(serviceItem);
            await _context.SaveChangesAsync();
            return Ok(serviceItem);

        }
        [HttpPut("UpdateServiceItem/{id}")]

        public async Task<IActionResult> UpdateServiceItem([FromBody] Service_I updateService, decimal id)
        {
            if(updateService == null || id != updateService.TransID)
            {
                return BadRequest("Invalid data");
            }

            var existingService = await _context.Service_Is.FindAsync(id);

            if (existingService == null)
            {
                return BadRequest("Services not found");
            }

            bool duplicateItem = await _context.Service_Is.AnyAsync(s => s.Itemcode == updateService.Itemcode && s.TransID != id);
            if (duplicateItem)
            {
                return Conflict("Data already existed");
            }

            existingService.Description= updateService.Description;
            existingService.Rt= updateService.Rt;
            
            _context.Service_Is.Update(existingService);
            await _context.SaveChangesAsync();
            return Ok(existingService);
        }
    }
}
