using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Repositories;

namespace TaskManagementSystem_API.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
            try
            {
                return await _noteRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while retrieving notes.", ex);
            }
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            try
            {
                return await _noteRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while retrieving the note with ID {id}.", ex);
            }
        }

        public async Task AddNoteAsync(Note note)
        {
            try
            {
                var existingNote = await _noteRepository.GetByContentAndTaskAsync(note.Content, note.TaskId);
                if (existingNote != null)
                {
                    throw new InvalidOperationException("A note with this content already exists for the task.");
                }
                await _noteRepository.AddAsync(note);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while adding the note.", ex);
            }
        }

        public async Task UpdateNoteAsync(Note note)
        {
            try
            {
                await _noteRepository.UpdateAsync(note);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("An error occurred while updating the note.", ex);
            }
        }

        public async Task DeleteNoteAsync(int id)
        {
            try
            {
                await _noteRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception($"An error occurred while deleting the note with ID {id}.", ex);
            }
        }
    }
}
