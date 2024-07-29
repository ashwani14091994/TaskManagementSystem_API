using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_API.Data;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TaskManagementAPIDbContext _context;

        public EmployeeRepository(TaskManagementAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while retrieving employees.", ex);
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the employee with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the employee.", ex);
            }
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }
        public async Task<Employee> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Employees
                    .FirstOrDefaultAsync(e => e.Email == email);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the employee with email {email}.", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while deleting the employee with ID {id}.", ex);
            }
        }
    }
}
