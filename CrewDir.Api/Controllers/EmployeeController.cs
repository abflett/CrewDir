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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(EmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<EmployeeResponse>))]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = _mapper.Map<List<EmployeeResponse>>(await _employeeRepository.Employees());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Management")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(EmployeeResponse))]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRequest employee)
        {
            try
            {
                var addedEmployee = _mapper.Map<EmployeeResponse>(await _employeeRepository.AddEmployee(_mapper.Map<Employee>(employee)));
                return CreatedAtAction(nameof(EmployeeById), new { id = addedEmployee.Id }, addedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeResponse))]
        public async Task<IActionResult> EmployeeById(int id)
        {
            try
            {
                var employee = _mapper.Map<EmployeeResponse>(await _employeeRepository.EmployeeById(id));
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(EmployeeResponse))]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRequest employee)
        {
            try
            {
                var updatedEmployee = _mapper.Map<EmployeeResponse>(await _employeeRepository.UpdateEmployee(_mapper.Map<Employee>(employee)));
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Management")]
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.RemoveEmployee(id);

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
