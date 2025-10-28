using Newtonsoft.Json;
using System.ComponentModel;

namespace Comprehension.Models
{
    public class Note
    {
        public required Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public required DateTime CreatedAt { get; set; }

        public required DateTime UpdatedAt { get; set; }
    }
}
