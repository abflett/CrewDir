using CrewDir.Api.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrewDir.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementRepository _managementRepository;

        public ManagementController(ManagementRepository managementRepository)
        {
            _managementRepository = managementRepository;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> AddManager(string token)
        {
            var result = await _managementRepository.AddManager(token, User.Identity!.Name!);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
