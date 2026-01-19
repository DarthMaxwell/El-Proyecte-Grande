using MBW.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBW.Server.Controllers;

[Route("api/[controller]")]

public class PostsController : Controller
{
    // GET: api/posts
    [HttpGet]
    public List<Post> GetAllPosts()
    {
        return null;
    }
    
    // GET: api/post?{userId} i think is it
    [HttpGet("{userId}")]
    public List<Post> GetUserPosts(int userId)
    {
        return null;
    }
    
    // POST:
    
    
    // PUT:
    
    // DELETE:
}