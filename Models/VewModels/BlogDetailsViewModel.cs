using Blog_Website.Models.Domain;

namespace Blog_Website.Models.VewModels
{
    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string PageContent { get; set; }

        public string ShortDecription { get; set; }

        public string FeaturedImage { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishDate { get; set; }
        public string Author { get; set; } = null!;

        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public int TotalLikes { get; set; }

        public bool Liked { get; set; }

        public string commentDescription { get; set; }

        public IEnumerable<BlogComment> Comments { get; set; }

    }
}
