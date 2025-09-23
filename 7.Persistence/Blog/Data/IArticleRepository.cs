using Blog.Models;

namespace Blog.Data
{
    public interface IArticleRepository
    {
        /// <summary>
        /// Retrieves all existing articles.
        /// </summary>
        /// <returns>
        /// A collection of articles. If no articles exist, returns an empty collection.
        /// </returns>
        IEnumerable<Article> GetAll();

        /// <summary>
        /// Retrieves a collection of articles published within the specified date range.
        /// </summary>
        /// <remarks>The date range is inclusive, meaning articles published exactly on the <paramref
        /// name="startDate"/> or <paramref name="endDate"/>  will be included in the result.</remarks>
        /// <param name="startDate">The start of the date range. Only articles published on or after this date will be included.</param>
        /// <param name="endDate">The end of the date range. Only articles published on or before this date will be included.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Article"/> objects representing the articles published within
        /// the specified date range. If no articles are found, the collection will be empty.</returns>
        IEnumerable<Article> GetByDateRange(DateTimeOffset startDate, DateTimeOffset endDate);

        /// <summary>
        /// Retrieves an article by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the article to retrieve. Must be a positive integer.</param>
        /// <returns>The <see cref="Article"/> object if an article with the specified <paramref name="id"/> exists; otherwise,
        /// <see langword="null"/>.</returns>
        Article? GetById(int id);

        /// <summary>
        /// Writes a new article to the repository.
        /// </summary>
        /// <param name="article">The <see cref="Article"/> to save.</param>
        /// <returns>The created <see cref="Article"/></returns>
        Article Create(Article article);

        /// <summary>
        /// Retrieves a collection of comments associated with the specified article.
        /// </summary>
        /// <param name="articleId">The unique identifier of the article for which to retrieve comments. Must be a positive integer.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Comment"/> objects representing the comments for the specified
        /// article. Returns an empty collection if no comments are found.</returns>
        IEnumerable<Comment> GetCommentsByArticleId(int articleId);

        /// <summary>
        /// Adds a new comment to the system.
        /// </summary>
        /// <remarks>
        /// The <see cref="Comment.ArticleId"/> property of the provided <paramref name="comment"/> object must be a valid
        /// identifier of an existing article in the repository. If it does not, the method should throw an <see cref="ArgumentException"/>.
        /// </remarks>
        /// <param name="comment">The comment to add. This parameter cannot be null.</param>
        void AddComment(Comment comment);
    }
}
