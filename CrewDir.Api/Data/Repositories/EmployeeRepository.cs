using CrewDir.Api.Data.Identity;
using CrewDir.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrewDir.Api.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public EmployeeRepository(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<AppUser>> Users()
        {
            return await _userManager.Users.Include(x => x.Employee).ToListAsync();
        }

        public async Task<List<Employee>> Employees()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> EmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                return employee;
            }

            throw new Exception("Could not find employee by the id.");
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _dbContext.Employees.AddAsync(employee);

            if (result.Entity != null)
            {
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("Failed to add employee.");
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = _dbContext.Employees.Update(employee);

            if (result.Entity != null)
            {
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("Could not update employee");
        }

        public async Task<bool> RemoveEmployee(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new Exception("Failed to remove employee.");
        }
    }
}
