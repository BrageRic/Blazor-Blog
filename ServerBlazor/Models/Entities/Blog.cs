using Microsoft.AspNetCore.Identity;

namespace ServerBlazor.Models.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public virtual List<Post>? Posts { get; set; }
        public virtual IdentityUser? Owner { get; set; }
    }
}
