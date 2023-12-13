using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ServerBlazor.Models.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        //Navigational Properties
        public virtual List<Comment>? Comments { get; set; } = new ();
        public virtual List<Tag>? Tags { get; set; } = new();
        public int BlogId { get; set; }
        public virtual Blog? Blog { get; set; }
        public virtual IdentityUser? Owner { get; set; }
    }

    public class PostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public List<Tag> Tags { get; set; } = new ();
    }
}
