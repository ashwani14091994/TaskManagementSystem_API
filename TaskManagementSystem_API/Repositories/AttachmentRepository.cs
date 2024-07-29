using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TaskManagementSystem_API.Data;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly TaskManagementAPIDbContext _context;

        public AttachmentRepository(TaskManagementAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttachmentDetail>> GetAllAsync()
        {
            try
            {
                return await _context.AttachmentDetails.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while retrieving attachments.", ex);
            }
        }

        public async Task<AttachmentDetail> GetByIdAsync(int id)
        {
            try
            {
                return await _context.AttachmentDetails.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the attachment with ID {id}.", ex);
            }
        }

        public async Task AddAsync(AttachmentDetail attachment)
        {
            try
            {
                _context.AttachmentDetails.Add(attachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the attachment.", ex);
            }
        }

        public async Task UpdateAsync(AttachmentDetail attachment)
        {
            try
            {
                _context.AttachmentDetails.Update(attachment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while updating the attachment.", ex);
            }
        }
        public async Task<AttachmentDetail> GetByFilePathAndTaskAsync(string filePath, int taskId)
        {
            try
            {
                return await _context.AttachmentDetails
                    .FirstOrDefaultAsync(a => a.FilePath == filePath && a.TaskId == taskId);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the attachment with file path '{filePath}' for task ID {taskId}.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var attachment = await _context.AttachmentDetails.FindAsync(id);
                if (attachment != null)
                {
                    _context.AttachmentDetails.Remove(attachment);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while deleting the attachment with ID {id}.", ex);
            }
        }
    }
}
