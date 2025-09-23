using Microsoft.Data.Sqlite;

namespace SQLiteSample
{
    /// <summary>
    /// Repository for managing todo items using SQLite database operations.
    /// This class demonstrates raw SQL usage without ORM.
    /// </summary>
    public class TodoRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the TodoRepository class.
        /// </summary>
        /// <param name="connectionString">The SQLite database connection string.</param>
        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        /// <summary>
        /// Creates the database schema if it doesn't exist.
        /// </summary>
        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS TodoItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    IsCompleted INTEGER NOT NULL DEFAULT 0,
                    CreatedAt TEXT NOT NULL,
                    CompletedAt TEXT
                )";

            using var command = new SqliteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Creates a new todo item in the database.
        /// </summary>
        /// <param name="todoItem">The todo item to create.</param>
        /// <returns>The created todo item with the assigned ID.</returns>
        public TodoItem Create(TodoItem todoItem)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var insertQuery = @"
                INSERT INTO TodoItems (Title, Description, IsCompleted, CreatedAt, CompletedAt)
                VALUES (@Title, @Description, @IsCompleted, @CreatedAt, @CompletedAt);
                SELECT last_insert_rowid();";

            using var command = new SqliteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Title", todoItem.Title);
            command.Parameters.AddWithValue("@Description", todoItem.Description ?? string.Empty);
            command.Parameters.AddWithValue("@IsCompleted", todoItem.IsCompleted ? 1 : 0);
            command.Parameters.AddWithValue("@CreatedAt", todoItem.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@CompletedAt", todoItem.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);

            var newId = Convert.ToInt32(command.ExecuteScalar());
            todoItem.Id = newId;

            return todoItem;
        }

        /// <summary>
        /// Retrieves all todo items from the database.
        /// </summary>
        /// <returns>A list of all todo items.</returns>
        public List<TodoItem> GetAll()
        {
            var todoItems = new List<TodoItem>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectQuery = "SELECT Id, Title, Description, IsCompleted, CreatedAt, CompletedAt FROM TodoItems ORDER BY CreatedAt DESC";

            using var command = new SqliteCommand(selectQuery, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var todoItem = new TodoItem
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    IsCompleted = reader.GetInt32(3) == 1,
                    CreatedAt = DateTime.Parse(reader.GetString(4)),
                    CompletedAt = reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5))
                };

                todoItems.Add(todoItem);
            }

            return todoItems;
        }

        /// <summary>
        /// Retrieves a todo item by its ID.
        /// </summary>
        /// <param name="id">The ID of the todo item to retrieve.</param>
        /// <returns>The todo item if found, otherwise null.</returns>
        public TodoItem? GetById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectQuery = "SELECT Id, Title, Description, IsCompleted, CreatedAt, CompletedAt FROM TodoItems WHERE Id = @Id";

            using var command = new SqliteCommand(selectQuery, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new TodoItem
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    IsCompleted = reader.GetInt32(3) == 1,
                    CreatedAt = DateTime.Parse(reader.GetString(4)),
                    CompletedAt = reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5))
                };
            }

            return null;
        }

        /// <summary>
        /// Updates an existing todo item in the database.
        /// </summary>
        /// <param name="todoItem">The todo item to update.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public bool Update(TodoItem todoItem)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var updateQuery = @"
                UPDATE TodoItems 
                SET Title = @Title, 
                    Description = @Description, 
                    IsCompleted = @IsCompleted, 
                    CompletedAt = @CompletedAt
                WHERE Id = @Id";

            using var command = new SqliteCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@Id", todoItem.Id);
            command.Parameters.AddWithValue("@Title", todoItem.Title);
            command.Parameters.AddWithValue("@Description", todoItem.Description ?? string.Empty);
            command.Parameters.AddWithValue("@IsCompleted", todoItem.IsCompleted ? 1 : 0);
            command.Parameters.AddWithValue("@CompletedAt", todoItem.CompletedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);

            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a todo item from the database.
        /// </summary>
        /// <param name="id">The ID of the todo item to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public bool Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var deleteQuery = "DELETE FROM TodoItems WHERE Id = @Id";

            using var command = new SqliteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@Id", id);

            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        /// <summary>
        /// Marks a todo item as completed or incomplete.
        /// </summary>
        /// <param name="id">The ID of the todo item to toggle.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool ToggleComplete(int id)
        {
            var todoItem = GetById(id);
            if (todoItem == null)
                return false;

            todoItem.IsCompleted = !todoItem.IsCompleted;
            todoItem.CompletedAt = todoItem.IsCompleted ? DateTime.Now : null;

            return Update(todoItem);
        }

        /// <summary>
        /// Gets the count of completed and pending todo items.
        /// </summary>
        /// <returns>A tuple containing (completedCount, pendingCount).</returns>
        public (int completedCount, int pendingCount) GetStats()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var statsQuery = @"
                SELECT 
                    SUM(CASE WHEN IsCompleted = 1 THEN 1 ELSE 0 END) as CompletedCount,
                    SUM(CASE WHEN IsCompleted = 0 THEN 1 ELSE 0 END) as PendingCount
                FROM TodoItems";

            using var command = new SqliteCommand(statsQuery, connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var completedCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                var pendingCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                return (completedCount, pendingCount);
            }

            return (0, 0);
        }
    }
}