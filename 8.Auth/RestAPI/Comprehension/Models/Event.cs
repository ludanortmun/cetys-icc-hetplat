using System.ComponentModel;

namespace Comprehension.Models
{
    public class Event
    {
        public required Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DateTime StartTime { get; set; }

        public required DateTime EndTime { get; set; }
    }
}
