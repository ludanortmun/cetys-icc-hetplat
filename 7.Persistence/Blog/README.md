# Blog Application

This is an ASP.NET Core MVC blog application that demonstrates data persistence patterns using both in-memory storage and SQLite database implementations. The application allows users to create articles, view them, and add comments, showcasing different persistence strategies for educational purposes.

## Purpose

This application demonstrates how to:
- Build an ASP.NET Core MVC web application with persistence
- Implement the Repository pattern for data access
- Use dependency injection to switch between different storage implementations
- Work with both in-memory and SQLite database persistence
- Handle CRUD operations for blog articles and comments
- Implement date-based filtering for articles
- Structure views and controllers for a blog-style application

## Features

The blog application provides the following functionality:

1. **View All Articles** - Display all published blog articles with basic information
2. **View Article Details** - Read full article content with associated comments
3. **Create New Articles** - Write and publish new blog posts with author information
4. **Add Comments** - Comment on existing articles
5. **Date Range Filtering** - Filter articles by publication date range
6. **Responsive Design** - Bootstrap-based UI that works on different screen sizes

## Architecture

The application follows the MVC (Model-View-Controller) architectural pattern with a clean separation of concerns:

### Models
- **Article** - Represents a blog article with properties like Id, Title, Content, AuthorName, AuthorEmail, and PublishedDate
- **Comment** - Represents comments on articles with ArticleId, Content, and PublishedDate
- **ArticleDetailsViewModel** - View model combining article and its comments for the details page

### Controllers
- **ArticlesController** - Handles all article-related operations (list, details, create, add comments)
- **HomeController** - Basic controller for general application pages

### Data Layer
The application implements the Repository pattern with two different persistence strategies:

- **IArticleRepository** - Interface defining the contract for article data operations
- **MemoryArticleRepository** - In-memory implementation using concurrent collections for thread safety
- **ArticleRepository** - SQLite database implementation (currently contains placeholder methods for implementation)

### Configuration
The application uses configuration-based switching between storage implementations:
- Set `UseInMemoryDatabase: true` in `appsettings.json` to use in-memory storage
- Set `UseInMemoryDatabase: false` to use SQLite database with the provided connection string

## Technologies Used

- **.NET 8.0** - Target framework
- **ASP.NET Core MVC** - Web application framework
- **Microsoft.Data.Sqlite 9.0.9** - SQLite database provider
- **Bootstrap** - CSS framework for responsive design
- **jQuery** - JavaScript library for enhanced UI interactions

## Project Structure

```
Blog/
├── Controllers/
│   ├── ArticlesController.cs    # Main controller for blog functionality
│   └── HomeController.cs        # Basic home controller
├── Data/
│   ├── IArticleRepository.cs    # Repository interface
│   ├── MemoryArticleRepository.cs    # In-memory implementation
│   └── ArticleRepository.cs     # SQLite implementation (placeholder)
├── Models/
│   ├── Article.cs               # Blog article model
│   ├── Comment.cs               # Comment model
│   ├── ArticleDetailsViewModel.cs    # View model for article details
│   └── ErrorViewModel.cs        # Error handling view model
├── Views/
│   ├── Articles/
│   │   ├── Index.cshtml         # Articles list view
│   │   ├── Details.cshtml       # Article details view
│   │   └── Create.cshtml        # Create article form
│   └── Shared/
│       └── _Layout.cshtml       # Application layout template
├── wwwroot/                     # Static files (CSS, JS, images)
├── appsettings.json             # Application configuration
└── Program.cs                   # Application entry point and configuration
```

## Running the Application

### Prerequisites
- .NET 8.0 SDK or later
- A web browser for testing the application

### Setup Instructions

1. Navigate to the Blog project directory:
   ```bash
   cd 7.Persistence/Blog
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the application:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your web browser and navigate to `https://localhost:5001` (or the URL shown in the console output)

### Configuration Options

The application behavior can be configured via `appsettings.json`:

```json
{
  "DatabaseConfig": {
    "UseInMemoryDatabase": true,
    "DefaultConnectionString": "DataSource=blog.db"
  }
}
```

- **UseInMemoryDatabase**: Set to `true` for in-memory storage (default), `false` for SQLite database
- **DefaultConnectionString**: SQLite database connection string (used when `UseInMemoryDatabase` is false)

## Usage

### Creating Articles
1. Click "New Article" in the navigation bar
2. Fill in the author information (name and email)
3. Enter the article title and content
4. Click "Create" to publish the article

### Viewing Articles
- The home page displays all published articles
- Click on an article title to view full details and comments
- Use date range query parameters to filter articles by publication date

### Adding Comments
1. Navigate to an article's details page
2. Scroll down to the comments section
3. Enter your comment in the text area
4. Click "Add Comment" to post your comment

### Filtering by Date
Add query parameters to the URL to filter articles by date range:
- `?startDate=2024-01-01` - Show articles from January 1, 2024 onwards
- `?endDate=2024-12-31` - Show articles up to December 31, 2024
- `?startDate=2024-01-01&endDate=2024-12-31` - Show articles within the specified date range

## Development Notes

### Current Implementation Status
- ✅ In-memory repository with full CRUD functionality
- ✅ MVC controllers and views for all features
- ✅ Configuration-based repository switching
- ⚠️ SQLite repository contains placeholder methods (throws `NotImplementedException`)

### Repository Pattern Implementation
The application demonstrates the Repository pattern benefits:
- **Abstraction** - Controllers depend on `IArticleRepository` interface, not concrete implementations
- **Testability** - Easy to mock the repository for unit testing
- **Flexibility** - Can switch between different storage implementations via configuration
- **Separation of Concerns** - Data access logic is separated from business logic

### Thread Safety
The `MemoryArticleRepository` uses:
- `ConcurrentDictionary` for thread-safe collections
- Lock objects for operations requiring atomicity
- Proper handling of concurrent read/write operations

## Learning Objectives

This application helps you understand:
- ASP.NET Core MVC application structure and conventions
- Repository pattern implementation and benefits
- Dependency injection in .NET applications
- Configuration-based application behavior
- Thread-safe programming with concurrent collections
- Data persistence strategies (in-memory vs. database)
- CRUD operations in web applications
- Date-based data filtering and querying

## Future Enhancements

Potential improvements that could be implemented:
- Complete the SQLite repository implementation
- Add article editing and deletion functionality
- Implement user authentication and authorization
- Add article search and tagging features
- Implement pagination for large numbers of articles
- Add rich text editing for article content
- Implement comment moderation features