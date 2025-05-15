using backend.Data;
using backend.DTOs;
using backend.Repository.Transaction;
using backend.Services.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialIssueNoteController : ControllerBase
    {
        private readonly IMaterialIssueNoteServices _service;
        private readonly ApplicationDbContext _context;
        private readonly IMaterialIssueNoteRepository _repository;

        public MaterialIssueNoteController(IMaterialIssueNoteServices service, ApplicationDbContext context, IMaterialIssueNoteRepository repository)
        {
            _service = service;
            _context = context;
            _repository = repository;
        }

        [HttpGet("GenerateISP")]

        public async Task<ActionResult<string>> GenerateISP()
        {
            try
            {
                var isp = await _repository.GenerateNewISPAsync();
                return Ok(isp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GenerateISP2")]

        public async Task<ActionResult<string>> GenerateISP2()
        {
            try
            {
                var isp = await _repository.GenerateNewISP2Async();
                return Ok(isp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetBranch")]

        public async Task<ActionResult<List<string>>> GetBranches()
        {
            try
            {
                var branches = await _repository.GetBranchNamesAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error:{ex.Message}");
            }
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

        [HttpGet("GetProduct")]

        public async Task<ActionResult<IEnumerable<string>>> GetProductNames()
        {
            var names = await _context.ProductMasters
                .Select(p => p.ProductName)
                .Distinct()
                .ToListAsync();
            return Ok(names);
        }

        [HttpGet("GetDetails")]

        public async Task<ActionResult<ProductInfoDto>> GetProductDetails(string productName)
        {
            var product = await _context.ProductMasters
                .Where(p => p.ProductName == productName).
                FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound("Product Not found");
            }
            var uomData = await _context.ProductUOMs
            .Where(u => u.ProductID == product.ProductID)
            .FirstOrDefaultAsync();

            var dto = new ProductInfoDto
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                Barcode = uomData?.Barcode ?? "",
                UOM = uomData?.UOM ?? "",
                UnitCost = (decimal)product.UnitRate
            };

            return Ok(dto);

        }

    }
}
