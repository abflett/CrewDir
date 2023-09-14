using AutoMapper;
using CrewDir.Api.Data.Repositories;
using CrewDir.Api.DTOs.Requests;
using CrewDir.Api.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CrewDir.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementRepository _managementRepository;
        private readonly IMapper _mapper;

        public ManagementController(ManagementRepository managementRepository, IMapper mapper)
        {
            _managementRepository = managementRepository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> AddManager(string token)
        {
            var result = await _managementRepository.AddManager(token, User.Identity!.Name!);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[Authorize(Roles = "Management")]
        [HttpPost("addManager")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> AddManagerRole(string id)
        {
            var result = await _managementRepository.AddManagerRole(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[Authorize(Roles = "Management")]
        [HttpPost("addUserToEmployee")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> AddUserToEmployee(UserEmployeeRequest userEmployeeRequest)
        {
            var result = await _managementRepository.AddUserToEmployee(userEmployeeRequest);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[Authorize(Roles = "Management")]
        [HttpPost("removeUserToEmployee")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> RemoveUserToEmployee(string id)
        {
            var result = await _managementRepository.RemoveUserToEmployee(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //[Authorize(Roles = "Management")]
        [HttpGet("users")]
        [ProducesResponseType(200, Type = typeof(List<AppUserResponse>))]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = _mapper.Map<List<AppUserResponse>>(await _managementRepository.Users());
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Management")]
        [HttpDelete("removeUser")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> RemoveUser(string id)
        {
            var result = await _managementRepository.RemoveUser(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
