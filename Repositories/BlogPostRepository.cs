﻿using Blog_Website.Data;
using Blog_Website.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Blog_Website.Models.VewModels;

namespace Blog_Website.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDbContext.AddAsync(blogPost);
            await blogDbContext.SaveChangesAsync();
            return blogPost;    
        }

        public async Task<BlogPost?> DeleteAsync(int id)
        {
           var existingBlog= await blogDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                blogDbContext.BlogPosts.Remove(existingBlog);
                await blogDbContext.SaveChangesAsync();
                return existingBlog;    
            } 
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return  await blogDbContext.BlogPosts.Include(x=> x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(int id)
        {
           return await blogDbContext.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync(x=>x.Id == id);
        }


        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog=await blogDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=> x.Id == blogPost.Id);

            if (existingBlog != null)
            { 
            existingBlog.Id= blogPost.Id;
                existingBlog.Heading= blogPost.Heading;
                existingBlog.PageTitle= blogPost.PageTitle;
                existingBlog.PageContent= blogPost.PageContent;
                existingBlog.ShortDecription= blogPost.ShortDecription;
                existingBlog.Author= blogPost.Author;
                existingBlog.FeaturedImage= blogPost.FeaturedImage;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishDate = blogPost.PublishDate;
                existingBlog.Visible= blogPost.Visible;
                existingBlog.Tags= blogPost.Tags;

                await blogDbContext.SaveChangesAsync();
                return existingBlog;



            }
            return null;
        }
        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
           return await blogDbContext.BlogPosts.Include(x => x.Tags).
                FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }
    }
}
