using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ServerBlazor.Models;
using ServerBlazor.Models.Entities;
using ServerBlazor.Data;
using ServerBlazor.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NuGet.Protocol;
using Microsoft.AspNetCore.SignalR;

namespace ServerBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class BlogAPIController : ControllerBase
    {
        private readonly IBlogRepository _repository;
        private readonly ApplicationDbContext _db;
        public IHubContext<NotiHub, INotiClient> _hubContext { get; }

        public BlogAPIController(IBlogRepository repository, ApplicationDbContext db, IHubContext<NotiHub, INotiClient> hubContext)
        {
            _repository = repository;
            _db = db;
            _hubContext = hubContext;
        }

        /// <summary>
        ///     Gets a specific post.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///         GET /api/BlogAPI/getPost/3
        /// </remarks>
        /// <param name="postId"></param>
        /// <response code="200">Returns the specified post</response>
        /// <response code="404">PostId not found</response>
        [AllowAnonymous]
        [HttpGet("getPost/{postId}")]
        public IActionResult GetPost([FromRoute] int postId)
        {
            if (!PostExists(postId))
            {
                return NotFound();
            }
            var post = _repository.GetPost(postId);

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
        /// <param name="blogId"></param>
        /// <response code="200">Returns a specific blog</response>
        /// <response code="404">BlogId not found</response>
        [AllowAnonymous]
        [HttpGet("getBlog/{blogId}")]
        public IActionResult GetBlog([FromRoute] int blogId)
        {
            if (!BlogExists(blogId))
            {
                return NotFound();
            }
            var blog = _repository.GetBlog(blogId);

            return Ok(blog);
        }

        /// <summary>
        ///     Gets a comments linked to a post.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///         GET /api/BlogAPI/getComments/3
        /// </remarks>
        /// <param name="postId"></param>
        /// <response code="200">Returns comments</response>
        /// <response code="404">PostId not found</response>
        [AllowAnonymous]
        [HttpGet("getComments/{postId}")]
        public IActionResult GetComments([FromRoute] int postId)
        {
            if (!PostExists(postId))
            {
                return NotFound();
            }
            var comments = _repository.GetCommentsByPost(postId);

            return Ok(comments);
        }

        // TODO: Possibly make DTO for the swagger example body?
        /// <summary>
        ///     Publish a new comment to specific post.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         POST /api/BlogAPI/newComment/3
        ///         {
        ///             "commentText": "string"
        ///         }
        /// </remarks>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <response code="201">Returns created comment</response>
        /// <response code="404">PostId not found</response>
        [Authorize]
        [HttpPost("newComment/{postId}")]
        public async Task<IActionResult> NewComment([FromBody][Bind("CommentText")] Comment comment, [FromRoute] int postId)
        {
            if (!PostExists(postId))
            {
                return NotFound();
            }
            comment.PostId = postId;
            await _repository.CreateComment(comment, User);
            await _hubContext.Clients.All.ReceiveComment();

            return CreatedAtAction("GetComments", new { postId }, comment);
        }

        /// <summary>
        ///     Publish a new comment to specific post.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         POST /api/BlogAPI/newPost
        ///         {
        ///             "Title": "string"
        ///             "Content": "string"
        ///         }
        /// </remarks>
        /// <param name="post"></param>
        /// <response code="201">Returns created post</response>
        [Authorize]
        [HttpPost("newPost")]
        public async Task<IActionResult> NewPost([FromBody][Bind("Title,Content")] Post post)
        {
            await _repository.CreatePost(post, User);
            await _hubContext.Clients.All.ReceivePost();

            return CreatedAtAction("GetPost", new { postId = post.PostId }, post);
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