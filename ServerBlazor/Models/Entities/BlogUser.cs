using System.ComponentModel.DataAnnotations;

namespace ServerBlazor.Models.Entities
{
    public class BlogUser
    {
        HttpClient HttpClient { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
