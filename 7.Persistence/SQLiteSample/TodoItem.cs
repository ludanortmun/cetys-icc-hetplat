namespace SQLiteSample
{
    /// <summary>
    /// Represents a todo item
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// The unique identifier for the todo item. Assigned at creation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the todo item.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The description of the todo item.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the todo item is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Represents the moment the todo item was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Represents the moment the todo item was completed (if applicable).
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}