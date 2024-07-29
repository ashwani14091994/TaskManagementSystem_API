using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem_API.Models
{
    public class AttachmentDetail
    {
        [Key]
        public int AttachmentId { get; set; } 
        [AttachmentFilePathValidation]
        public string FilePath { get; set; }
        [Required(ErrorMessage = "TaskId is required")]
        public int TaskId { get; set; }
        public virtual TaskDetail TaskDetails { get; set; }
    }
}
