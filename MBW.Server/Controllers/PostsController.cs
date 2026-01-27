using System.Data.Common;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly MBDBContext _dbContext;

    public PostsController(MBDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // GET: api/posts/{movieId}
    [HttpGet("{movieId}")]
    public async Task<ActionResult<List<Post>>> GetAllPostsForMovie(int movieId)
    {
        try
        {
            var result = await _dbContext.Posts.Where(p => p.MovieId == movieId).ToListAsync().ConfigureAwait(false);
            
            return Ok(result);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // POST: api/post
    // POST: api/reply
    // AUTHENTICATED USER
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePostDTO createPost)
    {
        try
        {
            Post p = new Post(createPost.UserId, createPost.MovieId, createPost.Content);
            
            _dbContext.Posts.Add(p);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            
            return Created(
                $"/api/posts/{p.MovieId}",
                p
            ); // 201 Created
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // PUT: api/post
    // AUTHENTICATED USER
    [HttpPut]
    public async Task<ActionResult<Post>> UpdatePost(PostDTO post)
    {
        try
        {
            // NEEDS USER VALIDATION
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (res == null)
                return NoContent(); // 204 No Content

            res.Content = post.Content;
            _dbContext.Posts.Update(res);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            
            return Ok(res); // 200 Ok
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // DELETE: api/post/{postId}
    // AUTHENTICATED USER
    [HttpDelete("{postId}")]
    public async Task<ActionResult> DeletePost(int postId)
    {
        try
        {
            // NEEDS USER VALIDATION
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            
            if (res == null)
                return NoContent(); // 204 No Content
            
            _dbContext.Posts.Remove(res);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            
            return Ok(res); // 200 Ok
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
}