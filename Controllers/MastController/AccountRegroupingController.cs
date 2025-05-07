using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/controller")]

public class AccountRegroupingController : ControllerBase

{
    private readonly ApplicationDbContext _context;

    public AccountRegroupingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("Account Type")]
    public async Task<IActionResult> ChangeAccountType([FromBody] Acc acc)
    {
        _context.Accs.Add(acc);
        await _context.SaveChangesAsync();
        return Ok(acc);
    }
}