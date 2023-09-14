using CrewDir.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CrewDir.Api.Data.Repositories
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> Departments()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> DepartmentById(int id)
        {
            var department = await _dbContext.Departments.Include(d => d.Employees!).FirstOrDefaultAsync(x => x.Id == id);

            var users = await _dbContext.Users.ToListAsync();

            if (department != null)
            {
                return department;
            }

            throw new Exception("Could not find department by the id.");
        }

        public async Task<List<Department>> SearchDepartments(string searchString)
        {
            return await _dbContext.Departments.Where(x => x.Name.ToUpper().Contains(searchString.ToUpper())).ToListAsync();
        }

        public async Task<Department> AddDepartment(Department department)
        {
            var result = await _dbContext.Departments.AddAsync(department);

            if (result.Entity != null)
            {
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("Could not add department.");
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var result = _dbContext.Departments.Update(department);

            if (result.Entity != null)
            {
                await _dbContext.SaveChangesAsync();
                return result.Entity;
            }

            throw new Exception("Could not update department.");
        }

        public async Task<bool> RemoveDepartment(int id)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (department != null)
            {
                _dbContext.Departments.Remove(department);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new Exception("Could not remove department.");
        }
    }
}
