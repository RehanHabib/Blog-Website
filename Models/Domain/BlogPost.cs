using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Website.Models.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string PageContent { get; set; }

        public string  ShortDecription  { get; set; }

        public string FeaturedImage { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishDate { get; set; }
        public string Author { get; set; } = null!;

        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<BlogPostLike> Likes { get; set; }

        public ICollection<BlogPostComment> Comments { get; set; }


    }
}
