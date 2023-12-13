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
        IEnumerable<Post> GetPostsByBlogId(int blogId);
        IEnumerable<Comment> GetCommentsByPost(int id);
        Task CreateBlog(IdentityUser user);
        Task CreatePost(Post post, IPrincipal principal);
        Task Update(int id, Post post);
        Task DeletePost(int postId);
        Task CreateComment(Comment comment, IPrincipal principal);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
        int BlogIdByUserId(string userId);
    }


}
