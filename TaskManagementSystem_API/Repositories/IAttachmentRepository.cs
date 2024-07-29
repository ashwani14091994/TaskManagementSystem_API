using System.Net.Mail;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<AttachmentDetail>> GetAllAsync();
        Task<AttachmentDetail> GetByIdAsync(int id);
        Task<AttachmentDetail> GetByFilePathAndTaskAsync(string filePath, int taskId);
        Task AddAsync(AttachmentDetail attachment);
        Task UpdateAsync(AttachmentDetail attachment);
        Task DeleteAsync(int id);
    }
}
