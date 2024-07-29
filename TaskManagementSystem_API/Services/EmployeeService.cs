using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Repositories;

namespace TaskManagementSystem_API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _employeeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while retrieving employees.", ex);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _employeeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while retrieving the employee with ID {id}.", ex);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetByEmailAsync(employee.Email);
                if (existingEmployee != null)
                {
                    throw new InvalidOperationException("An employee with this email already exists.");
                }
                await _employeeRepository.AddAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while adding the employee.", ex);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while deleting the employee with ID {id}.", ex);
            }
        }
    }
}
