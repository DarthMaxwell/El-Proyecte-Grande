using System.Data.Common;
using System.Security.Claims;
using MBW.Server.DTO;
using MBW.Server.Enum;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Authorization;
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
            List<Post> res = await _dbContext.Posts.Where(p => p.MovieId == movieId).ToListAsync();
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // GET: api/posts/user/{username}
    [HttpGet("user/{username}")]
    public async Task<ActionResult<List<Post>>> GetAllPostsForUser(string username)
    {
        try
        {
            User? u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (u == null)
                return NotFound("User not found");
            
            List<Post> res = await _dbContext.Posts.Where(p => p.Username == u.Name).ToListAsync();
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // GET: api/posts/post/{postId}
    [HttpGet("post/{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        try
        {
            Post? res = await _dbContext.Posts.FindAsync(id);
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // POST: api/posts
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePostDTO createPost)
    {
        try
        {
            var movieExists = await _dbContext.Movies.AnyAsync(m => m.Id == createPost.MovieId);
            if (!movieExists)
                return NotFound("Movie not found");
            
            Post p = new Post(createPost.MovieId, User.FindFirst(ClaimTypes.Name).Value, createPost.Title, createPost.Content);
            await _dbContext.Posts.AddAsync(p);
            await _dbContext.SaveChangesAsync();
            
            return Created(
                $"/api/posts/post/{p.Id}",
                p
            );
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // PUT: api/posts
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Post>> UpdatePost(PostDTO post)
    {
        try
        {
            User? u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (res == null)
                return NotFound();
            
            if (u == null || (u.Name != res.Username && u.Role != Roles.ADMIN))
                return Unauthorized("This is not your post");

            res.Title = post.Title;
            res.Content = post.Content;
            _dbContext.Posts.Update(res);
            await _dbContext.SaveChangesAsync();
            
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // DELETE: api/posts/{postId}
    [Authorize]
    [HttpDelete("{postId}")]
    public async Task<ActionResult> DeletePost(int postId)
    {
        try
        {
            User? u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

            if (res == null)
                return NotFound();
            
            if (u == null || (u.Name != res.Username && u.Role != Roles.ADMIN))
                return Unauthorized("This is not your post");
            
            _dbContext.Posts.Remove(res);
            await _dbContext.SaveChangesAsync();
            
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
}