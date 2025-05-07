using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction.ExcelImport
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICsvImportService _csvService;

        public VendorListController(ApplicationDbContext context, ICsvImportService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        [HttpPost("vendorimport")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                var records = await _csvService.ParseCsvAsync<Ex_Vend>(file);

                foreach (var item in records)
                    item.EntSysDate = DateTime.Now;

                await _context.Ex_Vends.AddRangeAsync(records);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Vendors imported", count = records.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

