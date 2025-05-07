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
