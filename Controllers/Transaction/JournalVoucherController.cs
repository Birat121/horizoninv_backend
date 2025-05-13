using backend.Data;
using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalVoucherController : ControllerBase
    {
        private readonly IJournalVoucherService _journalVoucherService;
        private readonly ApplicationDbContext _context;

        public JournalVoucherController(IJournalVoucherService journalVoucherService, ApplicationDbContext context)
        {
            _journalVoucherService = journalVoucherService;
            _context = context;
        }

        [HttpPost("journalentry")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] JVNEntryDto dto)
        {
            try
            {
                await _journalVoucherService.CreateJournalEntryAsync(dto);
                return Ok(new { message = "Journal entry created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred.", details = ex.Message });
            }
        }

        // POST: api/JournalVoucher/receipt
        [HttpPost("receipt")]
        public async Task<IActionResult> CreateReceiptVoucher([FromBody] SingleJVNEntryDto dto)
        {
            try
            {
                await _journalVoucherService.CreateSingleSideVoucherAsync(dto, "RV");
                return Ok(new { message = "Receipt voucher created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred.", details = ex.Message });
            }
        }

        // POST: api/JournalVoucher/payment
        [HttpPost("payment")]
        public async Task<IActionResult> CreatePaymentVoucher([FromBody] SingleJVNEntryDto dto)
        {
            try
            {
                await _journalVoucherService.CreateSingleSideVoucherAsync(dto, "PV");
                return Ok(new { message = "Payment voucher created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred.", details = ex.Message });
            }
        }

        [HttpGet("JournalNo")]
        public async Task<IActionResult> GetNextVoucherNo()
        {
            const string prefix = "JV81-";
            const int numberLength = 7;

            // Get the latest voucher ref starting with "JV81-"
            var lastVoucher = await _context.JVNs
                .Where(jv => jv.VoucherRef.StartsWith(prefix))
                .OrderByDescending(jv => jv.VoucherRef)
                .Select(jv => jv.VoucherRef)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastVoucher))
            {
                var numberPart = lastVoucher.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            string newVoucherNo = $"{prefix}{nextNumber.ToString($"D{numberLength}")}";

            return Ok(new { voucherNo = newVoucherNo });
        }

        [HttpGet("ReceiptVoucherNo")]
        public async Task<IActionResult> GetNextReceiptVoucherNo()
        {
            const string prefix = "RV81-";
            const int numberLength = 7;

            // Get the latest receipt voucher ref starting with "RV81-"
            var lastVoucher = await _context.JVNs
                .Where(rv => rv.VoucherRef.StartsWith(prefix))
                .OrderByDescending(rv => rv.VoucherRef)
                .Select(rv => rv.VoucherRef)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastVoucher))
            {
                var numberPart = lastVoucher.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            string newVoucherNo = $"{prefix}{nextNumber.ToString($"D{numberLength}")}";

            return Ok(new { voucherNo = newVoucherNo });
        }

        [HttpGet("PaymentVoucherNo")]
        public async Task<IActionResult> GetNextPaymentVoucherNo()
        {
            const string prefix = "PV81-";
            const int numberLength = 7;

            // Get the latest payment voucher ref starting with "PV81-"
            var lastVoucher = await _context.JVNs
                .Where(pv => pv.VoucherRef.StartsWith(prefix))
                .OrderByDescending(pv => pv.VoucherRef)
                .Select(pv => pv.VoucherRef)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(lastVoucher))
            {
                var numberPart = lastVoucher.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            string newVoucherNo = $"{prefix}{nextNumber.ToString($"D{numberLength}")}";

            return Ok(new { voucherNo = newVoucherNo });
        }





        [HttpGet("AccountName")]
        public async Task<IActionResult> GetAccountNames()
        {
            var names = await _context.Accs
                .Where(a => a.AclD == 1)               // ✅ Filter by Acl1 = 1
                .Select(a => a.Acn)
                .Distinct()
                .OrderBy(name => name)
                .ToListAsync();

            return Ok(names);
        }

    }
}

