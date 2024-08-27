using System.ComponentModel.DataAnnotations;

namespace Blog_Website.Models.VewModels
{
    public class AddTagRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

    }
}
