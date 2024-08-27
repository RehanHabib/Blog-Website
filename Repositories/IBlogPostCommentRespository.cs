using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface IBlogPostCommentRespository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GeCommentsByBlogIdAsync(int blogPostId);
    }
}
