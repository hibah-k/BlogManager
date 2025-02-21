using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BlogPostManager.Models;
using BlogPostManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BlogPostManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostService postService, ILogger<PostsController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postService.GetAllAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching posts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                if (post == null)
                    return NotFound();

                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postService.CreateAsync(post);
                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating post");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("bulkAdd")]
        public async Task<IActionResult> CreatePostsBulk([FromBody] IEnumerable<Post> posts)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                foreach (var post in posts)
                {
                    await _postService.CreateAsync(post);
                }
                return CreatedAtAction(nameof(GetPosts), posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating posts in bulk");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updatedPost.Id)
                return BadRequest("ID mismatch");

            try
            {
                var post = await _postService.GetByIdAsync(id);
                if (post == null)
                    return NotFound();

                post.Title = updatedPost.Title;
                post.Content = updatedPost.Content;
                post.UpdatedAt = DateTime.UtcNow; 

                await _postService.UpdateAsync(post);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("bulkUpdate")]
        public async Task<IActionResult> UpdatePostsBulk([FromBody] IEnumerable<Post> posts)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                foreach (var updatedPost in posts)
                {
                    var existingPost = await _postService.GetByIdAsync(updatedPost.Id);
                    if (existingPost != null)
                    {
                        existingPost.Title = updatedPost.Title;
                        existingPost.Content = updatedPost.Content;
                        existingPost.UpdatedAt = DateTime.UtcNow;

                        await _postService.UpdateAsync(existingPost);
                    }
                    else
                    {
                        _logger.LogWarning($"Post with ID {updatedPost.Id} not found during bulk update.");
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating posts in bulk");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var post = await _postService.GetByIdAsync(id);
                if (post == null)
                    return NotFound();

                await _postService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting post with id {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
