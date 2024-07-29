using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Repositories;

namespace TaskManagementSystem_API.Services
{
    public class AttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<IEnumerable<AttachmentDetail>> GetAttachmentsAsync()
        {
            try
            {
                return await _attachmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while retrieving attachments.", ex);
            }
        }

        public async Task<AttachmentDetail> GetAttachmentByIdAsync(int id)
        {
            try
            {
                return await _attachmentRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while retrieving the attachment with ID {id}.", ex);
            }
        }

        public async Task AddAttachmentAsync(AttachmentDetail attachment)
        {
            try
            {
                var existingAttachment = await _attachmentRepository.GetByFilePathAndTaskAsync(attachment.FilePath, attachment.TaskId);
                if (existingAttachment != null)
                {
                    throw new InvalidOperationException("An attachment with this file path already exists for the task.");
                }
                await _attachmentRepository.AddAsync(attachment);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while adding the attachment.", ex);
            }
        }

        public async Task UpdateAttachmentAsync(AttachmentDetail attachment)
        {
            try
            {
                await _attachmentRepository.UpdateAsync(attachment);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while updating the attachment.", ex);
            }
        }

        public async Task DeleteAttachmentAsync(int id)
        {
            try
            {
                await _attachmentRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while deleting the attachment with ID {id}.", ex);
            }
        }
    }
}
