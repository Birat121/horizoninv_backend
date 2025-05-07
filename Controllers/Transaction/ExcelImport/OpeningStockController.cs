using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction.ExcelImport
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningStockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICsvImportService _csvService;

        public OpeningStockController(ApplicationDbContext context, ICsvImportService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        [HttpPost("importOpeningstk")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                var records = await _csvService.ParseCsvAsync<Ex_OpbStk>(file);

                foreach (var item in records)
                {
                    item.EntSysDate = DateTime.Now;
                }

                await _context.Ex_OpbStks.AddRangeAsync(records);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Opening stock imported successfully", count = records.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
