using Blog_Website.Models.VewModels;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Blog_Website.Models.Domain;
using OpenTracing.Tag;
using Microsoft.AspNetCore.Authorization;


namespace Blog_Website.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        public IBlogPostRepository BlogPostRepository { get; }



        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get Tags from respository
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
            };

            return View(model);
        }



        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                PageContent = addBlogPostRequest.Content,
                ShortDecription = addBlogPostRequest.ShortDescription,
                FeaturedImage = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            // Map Tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsInt = int.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsInt);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            // Mapping tags back to domain model
            blogPost.Tags = selectedTags;


            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
           var blogPosts= await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the blog post along with its related tags
            var blogPost = await blogPostRepository.GetAsync(id);

            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                // Map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    PageContent = blogPost.PageContent,
                    Author = blogPost.Author,
                    FeaturedImage = blogPost.FeaturedImage,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDecription = blogPost.ShortDecription,
                    PublishDate = blogPost.PublishDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }

            // If the blog post is not found, return to the list or another appropriate action
            return RedirectToAction("List");
        }


        [HttpPost]

        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest) 
        {
            var blogPostDomainModel = new BlogPost
            { 
            Id=editBlogPostRequest.Id,
            Heading = editBlogPostRequest.Heading,
            PageTitle=editBlogPostRequest.PageTitle,
            PageContent=editBlogPostRequest.PageContent,
            Author = editBlogPostRequest.Author,
            ShortDecription=editBlogPostRequest.ShortDecription,
            FeaturedImage=editBlogPostRequest.FeaturedImage,
            PublishDate=editBlogPostRequest.PublishDate,
            UrlHandle=editBlogPostRequest.UrlHandle,
            Visible=editBlogPostRequest.Visible,


            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (int.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await tagRepository.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectedTags;

            // Submit information to repository to update
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updatedBlog != null)
            {
                // Show success notification
                return RedirectToAction("Edit");
            }

            // Show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(id);

            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id });
        }

    }
}

