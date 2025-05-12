using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/controller")]
public class CreateAccountLedger : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CreateAccountLedger(ApplicationDbContext context)
    {
       _context = context;
    }

    [HttpGet("GetGroupCode")]
    public async Task<IActionResult> GetNextGroupCode()
    {
        // Fetch the latest group code starting with "HX"
        var lastCode = await _context.Accs
            .Where(a => a.Acc1.StartsWith("HX"))
            .OrderByDescending(a => a.Acc1)
            .Select(a => a.Acc1)
            .FirstOrDefaultAsync();

        string nextCode = "HX01"; // Default for first entry

        if (!string.IsNullOrEmpty(lastCode) && lastCode.Length > 2)
        {
            var numberPart = lastCode.Substring(2); // Get numeric part
            if (int.TryParse(numberPart, out int currentNumber))
            {
                nextCode = $"HX{(currentNumber + 1).ToString("D2")}";
            }
        }

        return Ok(new { nextCode });
    }


    [HttpPost("CreateGroup")]
    public async Task<IActionResult> CreateGroupAccount([FromBody] Acc acc)
    {
        _context.Accs.Add(acc);
        await _context.SaveChangesAsync();
        return Ok(acc);
    }

    [HttpPost("subgroup")]
    public async Task<IActionResult> CreateSubAccount([FromBody] Acc acc)
    {
        // Optional: Check if the GroupAccountId exists
        if (!await _context.Accs.AnyAsync(g => g.Acn == acc.AcType))
        {
            return BadRequest("Invalid GroupAccountId");
        }

        _context.Accs.Add(acc);
        await _context.SaveChangesAsync();
        return Ok(acc);
    }

   
}
