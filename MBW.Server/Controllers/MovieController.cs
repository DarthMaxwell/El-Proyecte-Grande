using System.Data.Common;
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
        try
        {
            List<Movie> res = await _dbContext.Movies.ToListAsync();
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // GET: api/movie/topfive
    [HttpGet("topfive")]
    public async Task<ActionResult<List<Movie>>> GetTopFive()
    {
        try
        {
            List<Movie> res = await _dbContext.Posts.GroupBy(p => p.MovieId).OrderByDescending(g => g.Count())
                .Take(5).Select(g => g.Key).Join(_dbContext.Movies, id => id, m => m.Id, (id, m) => m)
                .ToListAsync();

            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
}
