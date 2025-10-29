namespace Comprehension.DTOs
{
    public class ReminderPatchDto
    {
        public string? Message { get; set; }
        public DateTime? ReminderTime { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
