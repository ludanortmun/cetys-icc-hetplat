# SQLite Sample Console Application

This is a C# console application that demonstrates SQLite database usage with Microsoft.Data.Sqlite. It implements a simple CRUD (Create, Read, Update, Delete) to-do application without using any ORM (Object-Relational Mapping) framework.

## Purpose

This application showcases how to:
- Connect to a SQLite database using Microsoft.Data.Sqlite
- Execute raw SQL commands and queries in C#
- Implement CRUD operations without ORM
- Handle database schema creation and initialization
- Work with parameterized SQL queries to prevent SQL injection
- Manage database connections and resources properly

## Features

The application provides a console-based interface for managing todo items with the following features:

1. **Create new todo item** - Add a new task with title and description
2. **View all todo items** - Display all tasks ordered by creation date
3. **View todo item by ID** - Retrieve and display a specific task
4. **Update todo item** - Modify the title and description of existing tasks
5. **Toggle todo completion** - Mark tasks as completed or pending
6. **Delete todo item** - Remove tasks from the database
7. **View statistics** - Show completion statistics and metrics
8. **Exit** - Close the application

## Database Schema

The application uses a simple SQLite database with the following table structure:

```sql
CREATE TABLE IF NOT EXISTS TodoItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    IsCompleted INTEGER NOT NULL DEFAULT 0,
    CreatedAt TEXT NOT NULL,
    CompletedAt TEXT
)
```

## Project Structure

- **TodoItem.cs** - Model class representing a todo item
- **TodoRepository.cs** - Data access layer with SQLite operations
- **Program.cs** - Console application with user interface logic

## Technologies Used

- **.NET 8.0** - Target framework
- **Microsoft.Data.Sqlite 9.0.9** - SQLite database provider
- **SQLite** - Lightweight database engine

## Key SQLite Concepts Demonstrated

### 1. Database Connection Management
```csharp
using var connection = new SqliteConnection(_connectionString);
connection.Open();
```

### 2. Parameterized Queries
```csharp
var insertQuery = @"
    INSERT INTO TodoItems (Title, Description, IsCompleted, CreatedAt, CompletedAt)
    VALUES (@Title, @Description, @IsCompleted, @CreatedAt, @CompletedAt);
    SELECT last_insert_rowid();";

using var command = new SqliteCommand(insertQuery, connection);
command.Parameters.AddWithValue("@Title", todoItem.Title);
command.Parameters.AddWithValue("@Description", todoItem.Description ?? string.Empty);
```

### 3. Data Reading
```csharp
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
```

### 4. Schema Creation
```csharp
var createTableQuery = @"
    CREATE TABLE IF NOT EXISTS TodoItems (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Description TEXT,
        IsCompleted INTEGER NOT NULL DEFAULT 0,
        CreatedAt TEXT NOT NULL,
        CompletedAt TEXT
    )";
```

## Running the Application

1. Make sure you have .NET 8.0 SDK installed
2. Navigate to the SQLiteSample directory
3. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run
```

## Database File

The application creates a SQLite database file named `todo.db` in the application directory. This file will persist your todo items between application runs.

## Error Handling

The application includes proper error handling for:
- Database connection issues
- Invalid user input
- SQL execution errors
- Data type conversion errors

## Security Features

- **Parameterized queries** to prevent SQL injection attacks
- **Input validation** for user-provided data
- **Proper resource disposal** using `using` statements

## Learning Objectives

This sample application helps you understand:
- Raw SQL operations in .NET applications
- SQLite database management
- C# database programming patterns
- Console application development
- CRUD operation implementation
- Data validation and error handling

## No ORM Usage

This application intentionally avoids using Entity Framework or any other ORM to demonstrate:
- Direct SQL command execution
- Manual object-relational mapping
- Low-level database operations
- SqliteDataReader usage for data retrieval

This approach provides a deeper understanding of how ORMs work under the hood and gives you complete control over SQL execution.