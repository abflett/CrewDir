using CrewDir.Api.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace CrewDir.Api.Data.Repositories
{
    public class ManagementRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public ManagementRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<bool> AddManager(string secret, string name)
        {
            var storedManagerSecret = _configuration["ManagerSecret"];
            var foundUser = await _userManager.FindByNameAsync(name);

            if (foundUser == null || storedManagerSecret != secret)
            {
                return false;
            }

            if (!_roleManager.RoleExistsAsync("Management").Result)
            {
                var newRole = new IdentityRole() { Name = "Management" };
                await _roleManager.CreateAsync(newRole);
            }
            await _userManager.AddToRoleAsync(foundUser!, "Management");

            return true;
        }
    }
}
