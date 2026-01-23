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
    
    // GET: api/posts/{movieId}
    [HttpGet("{movieId}")]
    public async Task<ActionResult<List<Post>>> GetAllPostsForMovie(int movieId)
    {
        //TODO
        throw new NotImplementedException();
    }
    
    // GET: api/post/{userId}
    
    // POST: api/post
    
    // PUT: api/post
    
    // DELETE: api/post/{postId}
}