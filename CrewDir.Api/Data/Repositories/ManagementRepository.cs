using CrewDir.Api.Data.Identity;
using CrewDir.Api.DTOs.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrewDir.Api.Data.Repositories
{
    public class ManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public ManagementRepository(ApplicationDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<List<AppUser>> Users()
        {
            return await _userManager.Users.Include(x => x.Employee).ToListAsync();
        }

        public async Task<bool> AddUserToEmployee(UserEmployeeRequest userEmployeeRequest)
        {
            var foundUser = await _userManager.FindByIdAsync(userEmployeeRequest.UserId);
            var foundEmployee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == userEmployeeRequest.EmployeeId);

            if (foundUser == null || foundEmployee == null)
            {
                return false;
            }

            if (!_roleManager.RoleExistsAsync("Employee").Result)
            {
                var newRole = new IdentityRole() { Name = "Employee" };
                await _roleManager.CreateAsync(newRole);
            }

            foundUser.Employee = foundEmployee;
            await _userManager.AddToRoleAsync(foundUser!, "Employee");
            await _userManager.UpdateAsync(foundUser);

            return true;
        }

        public async Task<bool> RemoveUserToEmployee(string id)
        {
            var foundUser = await _userManager.FindByIdAsync(id);
            var foundEmployee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.AppUserId == id);

            if (foundUser == null || foundEmployee == null)
            {
                return false;
            }

            foundEmployee.AppUser = null;
            foundEmployee.AppUserId = null;
            _dbContext.Employees.Update(foundEmployee);
            await _dbContext.SaveChangesAsync();

            foundUser.Employee = null;
            await _userManager.RemoveFromRoleAsync(foundUser, "Employee");
            await _userManager.UpdateAsync(foundUser);

            return true;
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

        public async Task<bool> AddManagerRole(string id)
        {
            var foundUser = await _userManager.FindByIdAsync(id);

            if (foundUser == null)
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

        public async Task<bool> RemoveUser(string id)
        {
            var foundUser = await _userManager.FindByIdAsync(id);
            var foundEmployee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.AppUserId == id);

            if (foundUser == null)
            {
                return false;
            }


            if (foundEmployee != null)
            {
                foundEmployee.AppUser = null;
                foundEmployee.AppUserId = null;
                _dbContext.Employees.Update(foundEmployee);
                await _dbContext.SaveChangesAsync();
            }

            var result = await _userManager.DeleteAsync(foundUser);

            return result.Succeeded;
        }
    }
}
