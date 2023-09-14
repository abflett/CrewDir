using AutoMapper;
using CrewDir.Api.Data.Repositories;
using CrewDir.Api.DTOs.Requests;
using CrewDir.Api.DTOs.Responses;
using CrewDir.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrewDir.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(DepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = _mapper.Map<List<DepartmentResponse>>(await _departmentRepository.Departments());
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = _mapper.Map<DepartmentResponse>(await _departmentRepository.DepartmentById(id));
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchDepartment(string search)
        {
            try
            {
                var department = _mapper.Map<List<DepartmentResponse>>(await _departmentRepository.SearchDepartments(search));
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentRequest department)
        {
            try
            {
                var addedDepartment = await _departmentRepository.AddDepartment(_mapper.Map<Department>(department));
                return CreatedAtAction(nameof(GetDepartmentById), new { id = addedDepartment.Id }, addedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentRequest department)
        {
            try
            {
                var updatedDepartment = _mapper.Map<DepartmentResponse>(await _departmentRepository.UpdateDepartment(_mapper.Map<Department>(department)));
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
