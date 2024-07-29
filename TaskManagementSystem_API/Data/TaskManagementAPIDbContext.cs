using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Data
{
    public class TaskManagementAPIDbContext:DbContext
    {
        public TaskManagementAPIDbContext(DbContextOptions<TaskManagementAPIDbContext> options)
           : base(options)
        {
        }

        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<AttachmentDetail> AttachmentDetails { get; set; }
    }
}
