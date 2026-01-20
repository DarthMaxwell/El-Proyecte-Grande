using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;

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
    
    // Dummy movies
    List<Movie> movies = new List<Movie>
    { 
        new Movie("The Matrix", 136) { Id = 1 },
        new Movie("Inception", 148) { Id = 2 }, 
        new Movie("Interstellar", 169) { Id = 3 },
        new Movie("The Dark Knight", 152) { Id = 4 },
        new Movie("Fight Club", 139) { Id = 5 }
    };
        
    // GET: api/movie
    [HttpGet]
    public async Task<List<Movie>> Get()
    {
        return movies;
        // await _dbContext.Movies
    } 
}
