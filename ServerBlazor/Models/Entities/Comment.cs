using Microsoft.AspNetCore.Identity;

namespace ServerBlazor.Models.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        //Navigational Properties
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
        public virtual IdentityUser? Owner { get; set; }
    }
}
