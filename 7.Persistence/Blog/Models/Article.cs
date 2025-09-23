namespace Blog.Models
{
    /// <summary>
    /// Represents a blog article
    /// </summary>
    public class Article
    {
        /// <summary>
        /// The unique identifier for the article. Assigned at creation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the author who wrote the article.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The email of the author who wrote the article.
        /// </summary>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// The title of the article. Specified by the user.
        /// It is limited to 100 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The full content of the article. 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Represents the moment the article was published
        /// </summary>
        public DateTimeOffset PublishedDate { get; set; }
    }
}
