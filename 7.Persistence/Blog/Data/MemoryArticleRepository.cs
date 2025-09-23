using Blog.Models;
using System.Collections.Concurrent;

namespace Blog.Data
{
    /// <summary>
    /// Represents a basic in-memory implementation of the <see cref="IArticleRepository"/> interface 
    /// for testing purposes.
    /// </summary>
    public class MemoryArticleRepository : IArticleRepository
    {
        private readonly ConcurrentDictionary<int, Article> _articles = new();
        private readonly ConcurrentDictionary<int, List<Comment>> _comments = new();
        private int _lastArticleId;
        private readonly object _lockObject = new();

        public IEnumerable<Article> GetAll()
        {
            return _articles.Values.ToList();
        }

        public IEnumerable<Article> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return _articles.Values
                .Where(a => a.PublishedDate >= startDate && a.PublishedDate <= endDate)
                .ToList();
        }

        public Article? GetById(int id)
        {
            _articles.TryGetValue(id, out var article);
            return article;
        }

        public Article Create(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            lock (_lockObject)
            {
                article.Id = ++_lastArticleId;
                if (!_articles.TryAdd(article.Id, article))
                {
                    throw new InvalidOperationException("Failed to add article to repository.");
                }
            }

            return article;
        }

        public IEnumerable<Comment> GetCommentsByArticleId(int articleId)
        {
            if (!_articles.ContainsKey(articleId))
                return Enumerable.Empty<Comment>();

            _comments.TryGetValue(articleId, out var comments);
            return comments?.ToList() ?? Enumerable.Empty<Comment>();
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (!_articles.ContainsKey(comment.ArticleId))
                throw new ArgumentException("No article exists with the specified ID.", nameof(comment));

            lock (_lockObject)
            {
                _comments.AddOrUpdate(
                    comment.ArticleId,
                    new List<Comment> { comment },
                    (_, existingComments) =>
                    {
                        existingComments.Add(comment);
                        return existingComments;
                    });
            }
        }
    }
}
