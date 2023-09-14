using CrewDir.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CrewDir.Api.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
