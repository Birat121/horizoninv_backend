using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.SystemSecurityController
{

    [Route("api/controller")]
    [ApiController]
    public class PrinterSetupController :ControllerBase

    {
        private readonly ApplicationDbContext _context;

        public PrinterSetupController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("savePrinter")]
        public async Task<IActionResult> SavePrinter([FromBody] PrinterName printerName)
        {
            if (printerName == null)
            {
                return BadRequest("Invalid printer detail");
            }

            var existingPrinter = await _context.PrinterNames.FirstOrDefaultAsync(p => p.TransID == printerName.TransID);

            if (existingPrinter == null)
            {
                printerName.SettingDate = DateTime.UtcNow;
                _context.PrinterNames.Add(printerName);
            }
            else
            {
                existingPrinter.KOTPrinter = printerName.KOTPrinter;
                existingPrinter.BOTPrinter = printerName.BOTPrinter;
                existingPrinter.POSPrinter = printerName.POSPrinter;
                existingPrinter.SettingDate = DateTime.UtcNow;

                _context.PrinterNames.Update(existingPrinter);
            }

            await _context.SaveChangesAsync();
            return Ok(printerName);


        }
    }
}
