using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_API.Data;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementAPIDbContext _context;

        public TaskRepository(TaskManagementAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDetail>> GetAllAsync()
        {
            try
            {
                return await _context.TaskDetails
                    .Include(t => t.Notes)
                    .Include(t => t.AttachmentDetails)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while retrieving tasks.", ex);
            }
        }

        public async Task<TaskDetail> GetByIdAsync(int id)
        {
            try
            {
                return await _context.TaskDetails
                    .Include(t => t.Notes)
                    .Include(t => t.AttachmentDetails)
                    .FirstOrDefaultAsync(t => t.TaskId == id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while retrieving the task with ID {id}.", ex);
            }
        }

        public async Task AddAsync(TaskDetail task)
        {
            try
            {
                _context.TaskDetails.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while adding the task.", ex);
            }
        }

        public async Task UpdateAsync(TaskDetail task)
        {
            try
            {
                _context.TaskDetails.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while updating the task.", ex);
            }
        }
        public async Task<TaskDetail> GetByTitleAsync(string title)
        {
            try
            {
                return await _context.TaskDetails
                    .FirstOrDefaultAsync(t => t.Title == title);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the task with title {title}.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _context.TaskDetails.FindAsync(id);
                if (task != null)
                {
                    _context.TaskDetails.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while deleting the task with ID {id}.", ex);
            }
        }
    }
}
