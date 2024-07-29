using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("getAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while retrieving employees.", Detailed = ex.Message });
            }
        }

        [HttpGet("getEmployeeById/{id}")]       
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while retrieving the employee with ID {id}.", Detailed = ex.Message });
            }
        }

        [HttpPost]
        [Route("createEmployee")]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _employeeService.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while adding the employee.", Detailed = ex.Message });
            }
        }

        [HttpPut("updateEmployee/{id}")]     
        public async Task<IActionResult> UpdateEmployee([FromRoute]int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (id != employee.EmployeeId)
                {
                    return BadRequest("Employee ID mismatch");
                }
                await _employeeService.UpdateEmployeeAsync(employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while updating the employee.", Detailed = ex.Message });
            }
        }

        [HttpDelete("deleteEmployee/{id}")]       
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while deleting the employee with ID {id}.", Detailed = ex.Message });
            }
        }
    }
}
