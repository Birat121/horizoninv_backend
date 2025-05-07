using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers.MastController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Department/CreateDepartment
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> SaveDepartment([FromBody] DepartmentMast department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.DeptName))
            {
                return BadRequest("Invalid department data");
            }

            var lastDept = await _context.DepartmentMasts
                                         .OrderByDescending(d => d.DeptId)
                                         .FirstOrDefaultAsync();

            int newDeptId = (lastDept != null && int.TryParse(lastDept.DeptId, out int lastId))
                            ? lastId + 1
                            : 100;

            department.DeptId = newDeptId.ToString();
            department.EnteredDate = DateTime.UtcNow;
            department.EnteredBy = "system"; // set this dynamically as needed
            department.EnteredSys = Environment.MachineName; // or some identifier

            bool deptExists = await _context.DepartmentMasts
                                            .AnyAsync(d => d.DeptName == department.DeptName);

            if (deptExists)
            {
                return Conflict("Department name already exists");
            }

            _context.DepartmentMasts.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(SaveDepartment), new { id = department.TransID }, department);
        }

        // PUT: api/Department/UpdateDepartment/{id}
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(decimal id, [FromBody] DepartmentMast department)
        {
            if (department == null || id != department.TransID)
                return BadRequest("Invalid Department");

            var existingDepartment = await _context.DepartmentMasts.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound("Department not found");
            }

            // Check uniqueness of DeptName
            bool deptExists = await _context.DepartmentMasts.AnyAsync(d => d.DeptName == department.DeptName && d.TransID != id);
            if (deptExists)
            {
                return Conflict("Department name already exists");
            }

            // Update values
            existingDepartment.DeptName = department.DeptName;

            _context.DepartmentMasts.Update(existingDepartment);
            await _context.SaveChangesAsync();

            return Ok(existingDepartment);
        }
    }
}

