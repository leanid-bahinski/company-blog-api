using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyBlog.Models;
using CompanyBlog.Data;

namespace CompanyBlog.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly BlogContext _context;

        public NotesController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            return await _context.Notes.ToListAsync();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Note>> GetNote(long id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }
            
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutNote(long id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPost("post")]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNote(long id)
        {
            if (_context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(long id)
        {
            return (_context.Notes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
