using Blog_Website.Models.Domain;
using Blog_Website.Models.VewModels;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRespository blogPostCommentRespository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRespository blogPostCommentRespository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRespository = blogPostCommentRespository;
        }


        [HttpGet]
        public async Task <IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost=  await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();




            if (blogPost != null) 
            {
              var totalLikes =   await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if (signInManager.IsSignedIn(User)) 
                {
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlogs(blogPost.Id);

                    var userId = userManager.GetUserId(User);

                    if (userId != null) {

                       var likeFromUser = likesForBlog.FirstOrDefault(x => x.USertId == userId);
                        liked = likeFromUser != null;
                    }
                }

                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    PageContent = blogPost.PageContent,
                    Author = blogPost.Author,
                    FeaturedImage = blogPost.FeaturedImage,
                    Heading = blogPost.Heading,
                    PublishDate = blogPost.PublishDate,
                    ShortDecription = blogPost.ShortDecription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked

                };


            }

            return View(blogDetailsViewModel);
        }


        [HttpPost]

        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel) 
        {
            if (signInManager.IsSignedIn(User))
            {
                var userId = userManager.GetUserId(User); // Retrieves the user ID as a string

                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.commentDescription,
                    UserId = userId,  // Correctly assigns the user ID
                    DateAdded = DateTime.Now
                };

                await blogPostCommentRespository.AddAsync(domainModel);
                return RedirectToAction("Index", "Blogs",
                    new { urlHandle = blogDetailsViewModel.UrlHandle });
            }

            return View();
        }
    }
}
