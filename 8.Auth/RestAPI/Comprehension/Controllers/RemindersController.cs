using Comprehension.Data;
using Comprehension.DTOs;
using Comprehension.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comprehension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly ComprehensionContext _context;

        public RemindersController(ComprehensionContext context)
        {
            _context = context;
        }

        // GET: api/Reminders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reminder>>> GetReminder()
        {
            return await _context.Reminder.ToListAsync();
        }

        // GET: api/Reminders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reminder>> GetReminder(Guid id)
        {
            var reminder = await _context.Reminder.FindAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            return reminder;
        }

        // PUT: api/Reminders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReminder(Guid id, Reminder reminder)
        {
            // Ensures the ID is populated
            if (reminder.Id == default)
            {
                reminder.Id = id;
            }

            if (id != reminder.Id)
            {
                return BadRequest();
            }

            _context.Entry(reminder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
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

        // PATCH: api/Reminders/5
        [HttpPatch("{id}")]
        [Consumes("application/merge-patch+json")]
        public async Task<IActionResult> PatchReminder(Guid id, [FromBody] ReminderPatchDto patch)
        {
            var reminder = await _context.Reminder.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            // Apply the patch - only update fields that are provided
            if (patch.Message != null)
            {
                reminder.Message = patch.Message;
            }

            if (patch.ReminderTime.HasValue)
            {
                reminder.ReminderTime = patch.ReminderTime.Value;
            }

            if (patch.IsCompleted.HasValue)
            {
                reminder.IsCompleted = patch.IsCompleted.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReminderExists(id))
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

        // POST: api/Reminders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reminder>> PostReminder(Reminder reminder)
        {
            _context.Reminder.Add(reminder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReminder", new { id = reminder.Id }, reminder);
        }

        // DELETE: api/Reminders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(Guid id)
        {
            var reminder = await _context.Reminder.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            _context.Reminder.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReminderExists(Guid id)
        {
            return _context.Reminder.Any(e => e.Id == id);
        }
    }
}
