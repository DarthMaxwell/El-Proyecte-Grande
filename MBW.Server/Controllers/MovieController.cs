using MBW.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBW.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
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
    } 
}
