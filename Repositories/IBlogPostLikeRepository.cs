using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(int blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlogs(int blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
