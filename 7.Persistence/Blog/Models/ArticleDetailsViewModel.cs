namespace Blog.Models
{
    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public ArticleDetailsViewModel(Article article, IEnumerable<Comment> comments)
        {
            Article = article;
            Comments = comments;
        }
    }
}
