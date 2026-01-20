using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;

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
    
    // GET: api/posts
    [HttpGet]
    public List<Post> GetAllPosts()
    {
        return null;
    }
    
    // GET: api/post/{userId} i think is it
    [HttpGet("{userId}")]
    public List<Post> GetUserPosts(int userId)
    {
        return null;
    }
    
    // POST:
    
    
    // PUT:
    
    // DELETE:
}