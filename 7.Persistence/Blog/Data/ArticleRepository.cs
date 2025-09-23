using Blog.Models;
using Microsoft.Data.Sqlite;

namespace Blog.Data
{
    /// <summary>
    /// Implementation of <see cref="IArticleRepository"/> using SQLite as a persistence solution.
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        private readonly string _connectionString;

        public ArticleRepository(DatabaseConfig _config)
        {
            _connectionString = _config.DefaultConnectionString ?? throw new ArgumentNullException("Connection string not found");
        }

        /// <summary>
        /// Creates the necessary tables for this application if they don't exist already.
        /// Should be called once when starting the service.
        /// </summary>
        public void EnsureCreated()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public Article? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Article Create(Article article)
        {
            throw new NotImplementedException();
        }

        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
        {
            throw new NotImplementedException();
        }
    }
}
