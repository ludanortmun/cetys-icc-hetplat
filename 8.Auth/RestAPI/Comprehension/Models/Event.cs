namespace Comprehension.Models
{
    public class Event
    {
        public Guid Id { get; internal set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DateTime StartTime { get; set; }

        public required DateTime EndTime { get; set; }
    }
}
