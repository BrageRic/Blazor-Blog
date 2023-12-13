using ServerBlazor.Data;
using ServerBlazor.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ServerBlazor.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _manager;

        public BlogRepository(ApplicationDbContext db, UserManager<IdentityUser> manager)
        {
            _db = db;
            _manager = manager;
        }

        public Post GetPost(int id)
        {
            var p = _db.Post.Where(x => x.PostId == id)
                .Include(x => x.Owner)
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .FirstOrDefault();
            return p;
        }

        public Blog GetBlog(int id)
        {
            var b = _db.Blog.Find(id);
            return b;
        }

        public IEnumerable<Post> GetPostsByUsername(string username)
        {
            var userId = _db.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
            return _db.Post.Where(x => x.Owner.Id == userId).ToList();
        }

        public IEnumerable<Post> GetPostsByBlogId(int blogId)
        {
            return _db.Post.Where(x => x.BlogId == blogId).ToList();
        }

        public IEnumerable<Comment> GetCommentsByPost(int id)
        {
            return _db.Comment.Where(x => x.PostId == id).ToList();
        }

        public async Task CreateBlog(Blog blog, IPrincipal principal)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);
            blog.Owner = currentUser;
            _db.Blog.Add(blog);
            await _db.SaveChangesAsync();
        }

        public async Task CreatePost(Post post, IPrincipal principal)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);
            post.Owner = currentUser;
            post.BlogId = BlogIdByUserId(currentUser.Id);
            _db.Post.Add(post);
            await _db.SaveChangesAsync();
        }

        public async Task Update(int id, Post post)
        {
            var p = _db.Post.Find(id);
            p.Title = post.Title;
            p.Content = post.Content;
            p.Tags = post.Tags;
            _db.Post.Update(p);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePost(int postId)
        {
            var p = _db.Post.Find(postId);
            _db.Post.Remove(p);
            await _db.SaveChangesAsync();
        }

        public async Task CreateComment(Comment comment, IPrincipal principal)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);
            comment.Owner = currentUser;
            _db.Comment.Add(comment);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public int BlogIdByUserId(string userId)
        {
            return _db.Blog.Where(x => x.Owner.Id == userId).Select(x => x.BlogId).FirstOrDefault();
        }

    }
}