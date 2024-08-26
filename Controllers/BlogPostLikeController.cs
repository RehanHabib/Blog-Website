using Blog_Website.Models.Domain;
using Blog_Website.Models.VewModels;
using Blog_Website.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }




        [HttpPost]
        [Route("Add")] 
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest) 
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                USertId = addLikeRequest.UserId
            };

            await  blogPostLikeRepository.AddLikeForBlog(model);
           
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Int}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] int blogPostId) 
        {
          var totalLikes =  await blogPostLikeRepository.GetTotalLikes(blogPostId);

            return Ok(totalLikes);
        }
    }
}
