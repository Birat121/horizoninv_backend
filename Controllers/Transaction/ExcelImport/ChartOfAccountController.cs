using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction.ExcelImport
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartOfAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICsvImportService _csvService;

        public ChartOfAccountsController(ApplicationDbContext context, ICsvImportService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        [HttpPost("chartimport")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                var records = await _csvService.ParseCsvAsync<Ex_ChartOfAcc>(file);

                foreach (var item in records)
                    item.EntSysDate = DateTime.Now;

                await _context.Ex_ChartOfAccs.AddRangeAsync(records);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Imported successfully", count = records.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

