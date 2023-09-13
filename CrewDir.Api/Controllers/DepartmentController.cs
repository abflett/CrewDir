using CrewDir.Api.Data.Repositories;
using CrewDir.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrewDir.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            try
            {
                var departments = await _departmentRepository.Departments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentRepository.DepartmentById(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult<Department>> SearchDepartment(string search)
        {
            try
            {
                var department = await _departmentRepository.SearchDepartments(search);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment([FromBody] Department department)
        {
            try
            {
                var addedDepartment = await _departmentRepository.AddDepartment(department);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = addedDepartment.Id }, addedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Department>> UpdateDepartment([FromBody] Department department)
        {
            try
            {
                var updatedDepartment = await _departmentRepository.UpdateDepartment(department);
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDepartment(int id)
        {
            try
            {
                var result = await _departmentRepository.RemoveDepartment(id);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Department not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
