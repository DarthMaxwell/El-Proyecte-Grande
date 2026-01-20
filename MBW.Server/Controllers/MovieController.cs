using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly MBDBContext _dbContext;
    
    public MovieController(MBDBContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    // GET: api/movie
    [HttpGet]
    public async Task<ActionResult<List<Movie>>> Get()
    {
        List<Movie> res = await _dbContext.Movies.ToListAsync();
        
        return Ok(res);
    } 
}
