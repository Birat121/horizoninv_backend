using backend.DTOs;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialIssueNoteController :ControllerBase
    {
        private readonly IMaterialIssueNoteServices _service;

        public MaterialIssueNoteController(IMaterialIssueNoteServices service)
        {
            _service = service;
        }

        [HttpPost("createMaterial")]
        public async Task<IActionResult> CreateMaterialIssue([FromBody] MaterialIssueDto materialIssueDto)
        {
            try
            {
                await _service.AddMaterialIssueNote(materialIssueDto);
                return Ok(new { message = "Material Issue created successfully." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
