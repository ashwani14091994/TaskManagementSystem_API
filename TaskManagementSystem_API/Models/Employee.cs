using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem_API.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        public virtual ICollection<TaskDetail> TaskDetails { get; set; } = new List<TaskDetail>();
    }
}
