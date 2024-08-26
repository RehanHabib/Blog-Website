using Blog_Website.Models.Domain;

namespace Blog_Website.Models.VewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
        
    }
}
