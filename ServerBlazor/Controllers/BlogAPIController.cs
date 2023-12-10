using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ServerBlazor.Models;
using ServerBlazor.Models.Entities;
using ServerBlazor.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NuGet.Protocol;

namespace ServerBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BlogAPIController : ControllerBase
    {
        private readonly IBlogRepository _repository;
        private readonly ApplicationDbContext _db;

        public BlogAPIController(IBlogRepository repository, ApplicationDbContext db)
        {
            _repository = repository;
            _db = db;
        }

        /// <summary>
        ///     Gets a specific post.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///         GET /api/BlogAPI/getPost/3
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns the specified post</response>
        /// <response code="404">PostId not found</response>
        [AllowAnonymous]
        [HttpGet("getPost/{id}")]
        public IActionResult GetPost([FromRoute] int id)
        {
            if (!PostExists(id))
            {
                return NotFound();
            }
            var post = _repository.GetPost(id);

            return Ok(post);
        }

        /// <summary>
        ///     Gets a list of a users posts.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///         GET /api/BlogAPI/getUsersPosts?username=bot
        /// </remarks>
        /// <param name="username"></param>
        /// <response code="200">Returns list of a users posts</response>
        /// <response code="404">User not found</response>
        [AllowAnonymous]
        [HttpGet("getUsersPosts")]
        public IActionResult GetUsersPosts([FromQuery] string username)
        {
            if (!UserExists(username))
            {
                return NotFound();
            }
            var posts = _repository.GetPostsByUsername(username);

            return Ok(posts);
        }

        /// <summary>
        ///     Gets a specific blog.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///         GET /api/BlogAPI/getblog/3
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns a specific blog</response>
        /// <response code="404">BlogId not found</response>
        [AllowAnonymous]
        [HttpGet("getBlog/{id}")]
        public IActionResult GetBlog([FromRoute] int id)
        {
            if (!BlogExists(id))
            {
                return NotFound();
            }
            var post = _repository.GetBlog(id);

            return Ok(post);
        }

        // GET: api/BlogAPI/getComments/3
        [AllowAnonymous]
        [HttpGet("getComments/{id}")]
        public IActionResult GetComments([FromRoute] int id)
        {
            if (!PostExists(id))
            {
                return NotFound();
            }
            var comments = _repository.GetCommentsByPost(id);

            return Ok(comments);
        }

        // POST: api/BlogAPI/newComment/3
        [Authorize]
        [HttpPost("newComment/{id}")]
        public async Task<IActionResult> NewComment([FromBody][Bind("CommentText")] Comment comment, [FromRoute] int id)
        {
            if (!PostExists(id))
            {
                return NotFound();
            }
            comment.PostId = id;
            await _repository.Create(comment, User);

            return CreatedAtAction("GetComments", new { id }, comment);
        }

        // POST: api/BlogAPI/newPost 
        [Authorize]
        [HttpPost("newPost")]
        public async Task<IActionResult> NewPost([FromBody][Bind("Title,Content")] Post post)
        {
            await _repository.Create(post, User);

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        [Authorize]
        [HttpPost("initializeBlog")] // midlertidig. skal bli private
        public async Task<IActionResult> InitilizeBlog([FromBody][Bind("Name")] Blog blog)
        {
            await _repository.Create(blog, User);

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }

        private bool PostExists(int id)
        {
            return _db.Post.Any(x => x.PostId == id);
        }
        
        private bool BlogExists(int id)
        {
            return _db.Post.Any(x => x.BlogId == id);
        }

        private bool UserExists(string username)
        {
            return _db.Users.Any(x => x.UserName == username);
        }

    }
}