using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem_API.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(1000, ErrorMessage = "Content length cannot exceed 1000 characters")]        
        public string Content { get; set; }
        [Required(ErrorMessage = "TaskId is required")]
        public int TaskId { get; set; }
        public virtual TaskDetail TaskDetails { get; set; }
    }
}
