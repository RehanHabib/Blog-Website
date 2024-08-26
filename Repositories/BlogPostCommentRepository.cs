using Blog_Website.Data;
using Blog_Website.Models.Domain;

namespace Blog_Website.Repositories
{
    public class BlogPostCommentRepository: IBlogPostCommentRespository
    {
        private readonly BlogDbContext blogDbContext;

        public BlogPostCommentRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {

            await blogDbContext.BlogPostComment.AddAsync(blogPostComment);
            await blogDbContext.SaveChangesAsync();
            return blogPostComment;
        }
    }
}
