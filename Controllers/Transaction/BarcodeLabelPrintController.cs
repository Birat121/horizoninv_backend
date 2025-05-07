using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeLabelPrintController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BarcodeLabelPrintController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("BarcodePrint")]
        public async Task<IActionResult> CreateBarcode([FromBody] BarPR bar, [FromQuery] bool isPreview = false)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.PName))
                return BadRequest("Invalid or missing product name.");

            try
            {
                // Lookup product
                var product = await _context.ProductMasters
                    .FirstOrDefaultAsync(p => p.ProductName == bar.PName);

                if (product == null)
                    return NotFound($"Product '{bar.PName}' not found.");

                // Assign values
                bar.PID = product.ProductID;
                bar.PName = product.ProductName;
                bar.PDate = DateTime.UtcNow;

                // Save to BarPR
                await _context.BarPRs.AddAsync(bar);

                if (isPreview)
                {
                    // Save barcode image only in preview mode
                    var barImg = new BarImg
                    {
                        PartyName = "Generated", // or bar.EnteredBy or client name
                        PId = product.ProductID,
                        ItemDesc = product.ProductName,
                        Rate = bar.Rate,
                        Ph = GeneratePlaceholderBarcodeImage(), // Replace with actual barcode generation logic
                        MfgDt = DateTime.Now.ToString("yyyy-MM-dd"),
                        ExpDt = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd")
                    };

                    await _context.BarImgs.AddAsync(barImg);
                }

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = isPreview ? "Preview data saved to BarPR and BarImg." : "Barcode saved to BarPR only.",
                    showBarcode = isPreview,
                    barcodeData = isPreview ? Convert.ToBase64String(GeneratePlaceholderBarcodeImage()) : null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        private byte[] GeneratePlaceholderBarcodeImage()
        {
            // Replace with barcode generation logic later
            return System.Text.Encoding.UTF8.GetBytes("FakeBarcodeImg");
        }

    }
}

