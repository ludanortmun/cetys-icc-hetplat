using SQLiteSample;

namespace SQLiteSample
{
    public class Program
    {
        private static TodoRepository _todoRepository = null!;

        public static void Main(string[] args)
        {
            // Initialize the database connection
            var connectionString = "Data Source=todo.db";
            _todoRepository = new TodoRepository(connectionString);

            Console.WriteLine("=== SQLite Todo Console Application ===");
            Console.WriteLine("This application demonstrates SQLite usage with Microsoft.Data.Sqlite");
            Console.WriteLine("No ORM is used - only raw SQL commands and queries.\n");

            // Main application loop
            bool running = true;
            while (running)
            {
                DisplayMenu();
                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        CreateTodoItem();
                        break;
                    case "2":
                        ViewAllTodoItems();
                        break;
                    case "3":
                        ViewTodoItemById();
                        break;
                    case "4":
                        UpdateTodoItem();
                        break;
                    case "5":
                        ToggleTodoCompletion();
                        break;
                    case "6":
                        DeleteTodoItem();
                        break;
                    case "7":
                        ViewStats();
                        break;
                    case "8":
                    case "q":
                    case "quit":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create new todo item");
            Console.WriteLine("2. View all todo items");
            Console.WriteLine("3. View todo item by ID");
            Console.WriteLine("4. Update todo item");
            Console.WriteLine("5. Toggle todo completion");
            Console.WriteLine("6. Delete todo item");
            Console.WriteLine("7. View statistics");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");
        }

        private static void CreateTodoItem()
        {
            Console.WriteLine("\n=== Create New Todo Item ===");
            
            Console.Write("Enter title: ");
            var title = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty. Operation cancelled.\n");
                return;
            }

            Console.Write("Enter description (optional): ");
            var description = Console.ReadLine()?.Trim() ?? string.Empty;

            var todoItem = new TodoItem
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            try
            {
                var createdItem = _todoRepository.Create(todoItem);
                Console.WriteLine($"Todo item created successfully with ID: {createdItem.Id}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating todo item: {ex.Message}\n");
            }
        }

        private static void ViewAllTodoItems()
        {
            Console.WriteLine("\n=== All Todo Items ===");
            
            try
            {
                var todoItems = _todoRepository.GetAll();
                
                if (todoItems.Count == 0)
                {
                    Console.WriteLine("No todo items found.\n");
                    return;
                }

                foreach (var item in todoItems)
                {
                    DisplayTodoItem(item);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving todo items: {ex.Message}\n");
            }
        }

        private static void ViewTodoItemById()
        {
            Console.WriteLine("\n=== View Todo Item by ID ===");
            
            Console.Write("Enter todo item ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Operation cancelled.\n");
                return;
            }

            try
            {
                var todoItem = _todoRepository.GetById(id);
                if (todoItem == null)
                {
                    Console.WriteLine($"Todo item with ID {id} not found.\n");
                    return;
                }

                DisplayTodoItem(todoItem);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving todo item: {ex.Message}\n");
            }
        }

        private static void UpdateTodoItem()
        {
            Console.WriteLine("\n=== Update Todo Item ===");
            
            Console.Write("Enter todo item ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Operation cancelled.\n");
                return;
            }

            try
            {
                var todoItem = _todoRepository.GetById(id);
                if (todoItem == null)
                {
                    Console.WriteLine($"Todo item with ID {id} not found.\n");
                    return;
                }

                Console.WriteLine("Current todo item:");
                DisplayTodoItem(todoItem);
                Console.WriteLine();

                Console.Write($"Enter new title (current: {todoItem.Title}): ");
                var newTitle = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(newTitle))
                {
                    todoItem.Title = newTitle;
                }

                Console.Write($"Enter new description (current: {todoItem.Description}): ");
                var newDescription = Console.ReadLine()?.Trim();
                if (newDescription != null)
                {
                    todoItem.Description = newDescription;
                }

                if (_todoRepository.Update(todoItem))
                {
                    Console.WriteLine("Todo item updated successfully.\n");
                }
                else
                {
                    Console.WriteLine("Failed to update todo item.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating todo item: {ex.Message}\n");
            }
        }

        private static void ToggleTodoCompletion()
        {
            Console.WriteLine("\n=== Toggle Todo Completion ===");
            
            Console.Write("Enter todo item ID to toggle completion: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Operation cancelled.\n");
                return;
            }

            try
            {
                var todoItem = _todoRepository.GetById(id);
                if (todoItem == null)
                {
                    Console.WriteLine($"Todo item with ID {id} not found.\n");
                    return;
                }

                Console.WriteLine("Current todo item:");
                DisplayTodoItem(todoItem);
                Console.WriteLine();

                if (_todoRepository.ToggleComplete(id))
                {
                    var status = todoItem.IsCompleted ? "incomplete" : "complete";
                    Console.WriteLine($"Todo item marked as {status}.\n");
                }
                else
                {
                    Console.WriteLine("Failed to toggle todo item completion.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling todo item: {ex.Message}\n");
            }
        }

        private static void DeleteTodoItem()
        {
            Console.WriteLine("\n=== Delete Todo Item ===");
            
            Console.Write("Enter todo item ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format. Operation cancelled.\n");
                return;
            }

            try
            {
                var todoItem = _todoRepository.GetById(id);
                if (todoItem == null)
                {
                    Console.WriteLine($"Todo item with ID {id} not found.\n");
                    return;
                }

                Console.WriteLine("Todo item to delete:");
                DisplayTodoItem(todoItem);
                Console.WriteLine();

                Console.Write("Are you sure you want to delete this item? (y/N): ");
                var confirmation = Console.ReadLine()?.Trim().ToLower();
                
                if (confirmation == "y" || confirmation == "yes")
                {
                    if (_todoRepository.Delete(id))
                    {
                        Console.WriteLine("Todo item deleted successfully.\n");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete todo item.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Delete operation cancelled.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting todo item: {ex.Message}\n");
            }
        }

        private static void ViewStats()
        {
            Console.WriteLine("\n=== Todo Statistics ===");
            
            try
            {
                var (completedCount, pendingCount) = _todoRepository.GetStats();
                var totalCount = completedCount + pendingCount;

                Console.WriteLine($"Total items: {totalCount}");
                Console.WriteLine($"Completed: {completedCount}");
                Console.WriteLine($"Pending: {pendingCount}");
                
                if (totalCount > 0)
                {
                    var completionPercentage = (double)completedCount / totalCount * 100;
                    Console.WriteLine($"Completion rate: {completionPercentage:F1}%");
                }
                
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving statistics: {ex.Message}\n");
            }
        }

        private static void DisplayTodoItem(TodoItem todoItem)
        {
            var status = todoItem.IsCompleted ? "✓ COMPLETED" : "○ PENDING";
            var completedText = todoItem.CompletedAt.HasValue ? $" (completed: {todoItem.CompletedAt:yyyy-MM-dd HH:mm})" : "";
            
            Console.WriteLine($"ID: {todoItem.Id}");
            Console.WriteLine($"Title: {todoItem.Title}");
            Console.WriteLine($"Description: {(string.IsNullOrEmpty(todoItem.Description) ? "(none)" : todoItem.Description)}");
            Console.WriteLine($"Status: {status}{completedText}");
            Console.WriteLine($"Created: {todoItem.CreatedAt:yyyy-MM-dd HH:mm}");
        }
    }
}
