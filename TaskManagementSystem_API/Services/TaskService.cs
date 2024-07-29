using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Repositories;

namespace TaskManagementSystem_API.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDetail>> GetTasksAsync()
        {
            try
            {
                return await _taskRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while retrieving tasks.", ex);
            }
        }

        public async Task<TaskDetail> GetTaskByIdAsync(int id)
        {
            try
            {
                return await _taskRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while retrieving the task with ID {id}.", ex);
            }
        }

        public async Task AddTaskAsync(TaskDetail task)
        {
            try
            {
                var existingTask = await _taskRepository.GetByTitleAsync(task.Title);
                if (existingTask != null)
                {
                    throw new InvalidOperationException("A task with this title already exists.");
                }
                await _taskRepository.AddAsync(task);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while adding the task.", ex);
            }
        }

        public async Task UpdateTaskAsync(TaskDetail task)
        {
            try
            {
                await _taskRepository.UpdateAsync(task);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while updating the task.", ex);
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            try
            {
                await _taskRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while deleting the task with ID {id}.", ex);
            }
        }
    }
}
