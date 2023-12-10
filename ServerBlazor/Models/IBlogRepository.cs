using ServerBlazor.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace ServerBlazor.Models
{
    public interface IBlogRepository
    {
        Post GetPost(int id);
        Blog GetBlog(int id);
        IEnumerable<Post> GetPostsByUsername(string username);
        IEnumerable<Comment> GetCommentsByPost(int id);
        Task Create(Blog blog, IPrincipal principal);
        Task Create(Post post, IPrincipal principal);
        Task Update(Post post);
        Task Delete(Post post);
        Task Create(Comment comment, IPrincipal principal);
        Task Update(Comment comment);
        Task Delete(Comment comment);
    }


}
