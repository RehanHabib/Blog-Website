using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Website.Models.VewModels
{
    public class EditBlogPostRequest
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


        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();


    }
}
