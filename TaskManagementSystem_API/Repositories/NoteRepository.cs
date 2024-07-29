using Microsoft.EntityFrameworkCore;
using TaskManagementSystem_API.Data;
using TaskManagementSystem_API.Models;

namespace TaskManagementSystem_API.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly TaskManagementAPIDbContext _context;

        public NoteRepository(TaskManagementAPIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            try
            {
                return await _context.Notes.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while retrieving notes.", ex);
            }
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Notes.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the note with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Note note)
        {
            try
            {
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while adding the note.", ex);
            }
        }

        public async Task UpdateAsync(Note note)
        {
            try
            {
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while updating the note.", ex);
            }
        }
        public async Task<Note> GetByContentAndTaskAsync(string content, int taskId)
        {
            try
            {
                return await _context.Notes
                    .FirstOrDefaultAsync(n => n.Content == content && n.TaskId == taskId);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while retrieving the note with content '{content}' for task ID {taskId}.", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var note = await _context.Notes.FindAsync(id);
                if (note != null)
                {
                    _context.Notes.Remove(note);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception($"An error occurred while deleting the note with ID {id}.", ex);
            }
        }
    }
}
