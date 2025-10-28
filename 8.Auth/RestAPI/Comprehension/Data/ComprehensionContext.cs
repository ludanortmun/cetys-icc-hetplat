using Microsoft.EntityFrameworkCore;
using Comprehension.Models;

namespace Comprehension.Data
{
    public class ComprehensionContext : DbContext
    {
        public ComprehensionContext (DbContextOptions<ComprehensionContext> options)
            : base(options)
        {
        }

        public DbSet<Reminder> Reminder { get; set; } = default!;
        public DbSet<Event> Event { get; set; } = default!;
        public DbSet<Note> Note { get; set; } = default!;
    }
}
