using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BlogPostComment>> GeCommentsByBlogIdAsync(int blogPostId)
        {
            return await blogDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }
    }
}
