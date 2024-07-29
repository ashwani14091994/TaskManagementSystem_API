using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TaskManagementSystem_API.Models
{
    public class TaskDetail
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title length cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Due date is required")]        
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
        public virtual ICollection<AttachmentDetail> AttachmentDetails { get; set; } = new List<AttachmentDetail>();

    }
}
