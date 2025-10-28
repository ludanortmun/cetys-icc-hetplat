using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Comprehension.Models
{
    public class Reminder
    {
        public required Guid Id { get; set; }

        public required string Message { get; set; }

        public required DateTime ReminderTime { get; set; }

        public required bool IsCompleted { get; set; } = false;
    }
}
