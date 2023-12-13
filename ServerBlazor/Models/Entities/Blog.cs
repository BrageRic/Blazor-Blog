using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ServerBlazor.Models.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public virtual List<Post>? Posts { get; set; }
        [JsonIgnore]
        public virtual IdentityUser? Owner { get; set; }
    }
}
