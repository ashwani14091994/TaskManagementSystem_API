using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Services;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("getAllTasks")]
        public async Task<ActionResult<IEnumerable<TaskDetail>>> GetTasks()
        {
            try
            {
                var tasks = await _taskService.GetTasksAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while retrieving tasks.", Detailed = ex.Message });
            }
        }

        [HttpGet("getTaskById/{id}")]        
        public async Task<ActionResult<TaskDetail>> GetTask(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while retrieving the task with ID {id}.", Detailed = ex.Message });
            }
        }

        [HttpPost]
        [Route("createTask")]
        public async Task<ActionResult<TaskDetail>> CreateTask([FromBody] TaskDetail task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _taskService.AddTaskAsync(task);
                return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while adding the task.", Detailed = ex.Message });
            }
        }

        [HttpPut("updateTask/{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute]int id, [FromBody] TaskDetail task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.TaskId)
            {
                return BadRequest("Task ID mismatch.");
            }
            try
            {
                var existingNote = await _taskService.GetTaskByIdAsync(id);
                if (existingNote == null)
                {
                    return NotFound();
                }
                await _taskService.UpdateTaskAsync(task);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while updating the task.", Detailed = ex.Message });
            }
        }

        [HttpDelete("deleteTask/{id}")]      
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var existingNote = await _taskService.GetTaskByIdAsync(id);
                if (existingNote == null)
                {
                    return NotFound();
                }

                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while deleting the task with ID {id}.", Detailed = ex.Message });
            }
        }
    }
}
