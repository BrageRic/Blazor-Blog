using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServerBlazor.Models.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        //Navigational Properties
        public int PostId { get; set; }
        [JsonIgnore]
        public virtual Post? Post { get; set; }
        [JsonIgnore]
        public virtual IdentityUser? Owner { get; set; }
    }

    public class CommentDTO
    {
        [Required]
        public string CommentText { get; set; }
    }
}
