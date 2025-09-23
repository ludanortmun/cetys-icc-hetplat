namespace Blog.Models
{
    public class Comment
    {
        /// <summary>
        /// The identifier of the article this comment belongs to.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// The content of the comment.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Represents the moment the comment was posted.
        /// </summary>
        public DateTimeOffset PublishedDate { get; set; }
    }
}
