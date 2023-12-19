using ServerBlazor.Data;
using ServerBlazor.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public IEnumerable<Post> GetAllPosts()
        {
            return _db.Post
                .Include(x => x.Owner)
                .Include(x => x.Tags)
                .ToList();
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
            var b = _db.Blog.Where(x => x.BlogId == id)
                .Include(x => x.Posts)
                .Include(x => x.Owner)
                .FirstOrDefault();
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
            return _db.Comment.Where(x => x.PostId == id)
                .Include(x => x.Owner)
                .ToList();
        }

        public async Task CreateBlog(IdentityUser user)
        {
            var blog = new Blog()
            {
                Owner = user,
                Name = user.UserName
            };
            _db.Blog.Add(blog);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CreatePost(Post post, IPrincipal principal)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);
            post.Owner = currentUser;
            post.BlogId = BlogIdByUserId(currentUser.Id);
            _db.Post.Add(post);
            await _db.SaveChangesAsync();
            return post.PostId;
        }

        public async Task Update(int id, Post post)
        {
            var p = _db.Post.Find(id);
            p.Title = post.Title;
            p.Content = post.Content;

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

        public async Task DeleteComment(int commentId)
        {
            var c = await _db.Comment.FindAsync(commentId);
            _db.Comment.Remove(c);
            await _db.SaveChangesAsync();
        }

        public int BlogIdByUserId(string userId)
        {
            return _db.Blog.Where(x => x.Owner.Id == userId).Select(x => x.BlogId).FirstOrDefault();
        }

        public async Task AddTagsToPost(int postId, List<Tag> tags)
        {
            var p = await _db.Post.FindAsync(postId);
            p.Tags = new List<Tag>();
            foreach (var tag in tags)
            {
                if (TagExists(tag)) // Add existing tag to post.
                {
                    var t = _db.Tag.Where(x => x.TagText == tag.TagText).FirstOrDefault();
                    p.Tags.Add(t);
                }
                else // Create new tag and add it to post.
                {
                    p.Tags.Add(tag);
                }
            }
            _db.Post.Update(p);
            await _db.SaveChangesAsync();
        }

        public string GetUidByUsername(string username)
        {
            return _db.Users.Where(x => x.UserName == username).Select(x => x.Id).FirstOrDefault();
        }

        public bool PostExists(int id)
        {
            return _db.Post.Any(x => x.PostId == id);
        }

        public bool BlogExists(int id)
        {
            return _db.Post.Any(x => x.BlogId == id);
        }

        public bool UserExists(string username)
        {
            return _db.Users.Any(x => x.UserName == username);
        }
        private bool TagExists(Tag tag)
        {
            return _db.Tag.Any(x => x.TagText == tag.TagText);
        }

    }
}