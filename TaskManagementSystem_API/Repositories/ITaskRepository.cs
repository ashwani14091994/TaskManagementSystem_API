using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDetail>> GetAllAsync();
        Task<TaskDetail> GetByIdAsync(int id);
        Task<TaskDetail> GetByTitleAsync(string title);
        Task AddAsync(TaskDetail task);
        Task UpdateAsync(TaskDetail task);
        Task DeleteAsync(int id);
    }
}
