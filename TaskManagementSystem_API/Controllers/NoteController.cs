using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem_API.Models;
using TaskManagementSystem_API.Services;

namespace TaskManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Route("getAllNotes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            try
            {
                var notes = await _noteService.GetNotesAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while retrieving notes.", Detailed = ex.Message });
            }
        }

        [HttpGet("getNoteById/{id}")]       
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            try
            {
                var note = await _noteService.GetNoteByIdAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while retrieving the note with ID {id}.", Detailed = ex.Message });
            }
        }

        [HttpPost]
        [Route("createNote")]
        public async Task<ActionResult<Note>> CreateNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _noteService.AddNoteAsync(note);
                return CreatedAtAction(nameof(GetNote), new { id = note.NoteId }, note);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while adding the note.", Detailed = ex.Message });
            }
        }

        [HttpPut("updateNote/{id}")]      
        public async Task<IActionResult> UpdateNote([FromRoute]int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteId)
            {
                return BadRequest("Note ID mismatch.");
            }
            try
            {
                var existingNote = await _noteService.GetNoteByIdAsync(id);
                if (existingNote == null)
                {
                    return NotFound();
                }
                await _noteService.UpdateNoteAsync(note);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = "An error occurred while updating the note.", Detailed = ex.Message });
            }
        }

        [HttpDelete("deleteNote/{id}")]        
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var existingNote = await _noteService.GetNoteByIdAsync(id);
                if (existingNote == null)
                {
                    return NotFound();
                }
                await _noteService.DeleteNoteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                return StatusCode(500, new { Message = $"An error occurred while deleting the note with ID {id}.", Detailed = ex.Message });
            }
        }
    }
}
