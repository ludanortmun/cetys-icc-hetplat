namespace Comprehension.Models
{
    public class Note
    {
        public Guid Id { get; internal set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; internal set; }

        public DateTime UpdatedAt { get; internal set; }
    }
}
