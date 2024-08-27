using System.ComponentModel.DataAnnotations;

namespace Blog_Website.Models.VewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
      
        
        [Required]
        [MinLength(6, ErrorMessage ="Password needs to be atleast 6 characters")]
        public string  Password { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
