using Comprehension.Data;
using Comprehension.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Comprehension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ComprehensionContext _context;

        public EventsController(ComprehensionContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            // Ensures the ID is populated
            if (@event.Id == default)
            {
                @event.Id = id;
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            if (!IsValid(@event))
            {
                return BadRequest("Invalid data.");
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PATCH: api/Events/5
        [HttpPatch("{id}")]
        [Consumes("application/merge-patch+json")]
        public async Task<IActionResult> PatchEvent(Guid id, JsonElement patchData)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            // Apply the patch - only update fields that are present in the request
            if (patchData.TryGetProperty("title", out JsonElement titleElement))
            {
                @event.Title = titleElement.GetString() ?? @event.Title;
            }

            if (patchData.TryGetProperty("description", out JsonElement descriptionElement))
            {
                @event.Description = descriptionElement.GetString() ?? @event.Description;
            }

            if (patchData.TryGetProperty("startTime", out JsonElement startTimeElement))
            {
                if (startTimeElement.TryGetDateTime(out DateTime startTime))
                {
                    @event.StartTime = startTime;
                }
            }

            if (patchData.TryGetProperty("endTime", out JsonElement endTimeElement))
            {
                if (endTimeElement.TryGetDateTime(out DateTime endTime))
                {
                    @event.EndTime = endTime;
                }
            }

            // Validate after patch
            if (!IsValid(@event))
            {
                return BadRequest("Invalid data after applying patch.");
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            if (!IsValid(@event))
            {
                return BadRequest("Invalid e data.");
            }
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _context.Event.Any(e => e.Id == id);
        }

        private bool IsValid(Event e)
        {
            if (string.IsNullOrWhiteSpace(e.Title) || string.IsNullOrWhiteSpace(e.Description))
            {
                return false;
            }
            if (e.StartTime >= e.EndTime)
            {
                return false;
            }
            return true;
        }
    }
}
