using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialIssueNoteController :ControllerBase
    {
        private readonly MaterialIssueNoteServices _services;

        public MaterialIssueNoteController(MaterialIssueNoteServices services)
        {
            _services = services;
        }

        [HttpPost("createMaterial")]
        public async Task<IActionResult> CreateMaterialIssue([FromBody] MaterialIssueDto materialIssueDto)
        {
            try
            {
                await _services.AddMaterialIssueNote(materialIssueDto);
                return Ok(new { message = "Material Issue created successfully." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
