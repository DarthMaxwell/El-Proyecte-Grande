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
            List<Post> result = await _dbContext.Posts.Where(p => p.MovieId == movieId).ToListAsync();
            
            return Ok(result);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // POST: api/post
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePostDTO createPost)
    {
        try
        {
            Post p = new Post(createPost.UserId, createPost.MovieId, createPost.Content);
            
            await _dbContext.Posts.AddAsync(p);
            await _dbContext.SaveChangesAsync();
            
            return Created(
                $"/api/posts/{p.MovieId}",
                p
            );
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // PUT: api/post
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Post>> UpdatePost(PostDTO post)
    {
        try
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (res == null)
                return NotFound();
            
            if (user == null || (user.Id != res.UserId && user.Role != Roles.ADMIN))
                return Unauthorized("This is not your post");
            
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
    
    // DELETE: api/post/{postId}
    [Authorize]
    [HttpDelete("{postId}")]
    public async Task<ActionResult> DeletePost(int postId)
    {
        try
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Post? res = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

            if (res == null)
                return NotFound();
            
            if (user == null || (user.Id != res.UserId && user.Role != Roles.ADMIN))
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