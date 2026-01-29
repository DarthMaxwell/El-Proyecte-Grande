using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using System;
using MBW.Server.Controllers;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ServerTests;

public class MovieControllerTests
{
    private static MBDBContext CreateDb()
    {
        // Need a new db name for every test - using TestContext.CurrentContext.Test.ID
        var options = new DbContextOptionsBuilder<MBDBContext>()
            .UseInMemoryDatabase(TestContext.CurrentContext.Test.ID)
            .Options;

        return new MBDBContext(options);
    }

    [Test]
    public async Task Get_ReturnOkAndMovieList()
    {
        var db = CreateDb();
        db.Movies.AddRange(
            new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            },
            new Movie
            {
                Id = 2, ReleaseDate = new DateOnly(1972, 3, 24), Length = 175, Title = "The Godfather",
                Director = "Francis Ford Coppola", Genre = "Crime",
                Description = "The aging patriarch transfers control of his empire."
            }
        );
        await db.SaveChangesAsync();
        var controller = new MovieController(db);
        var result = await controller.Get();
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movies = ok.Value as List<Movie>;
        Assert.That(movies, Is.Not.Null);
        Assert.That(movies.Count, Is.EqualTo(2));
    }
    
    [Test]
    public async Task Get_NoMovies_ReturnOkAndEmptyList()
    {
        var db = CreateDb();
        var controller = new MovieController(db);
        var result = await controller.Get();
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movies = ok.Value as List<Movie>;
        Assert.That(movies, Is.Not.Null);
        Assert.That(movies.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetMovie_ReturnOkAndMovie()
    {
        var db = CreateDb();
        db.Movies.Add(new Movie
        {
            Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
            Director = "Frank Darabont", Genre = "Drama",
            Description = "Two imprisoned men bond over a number of years."
        });
        await db.SaveChangesAsync();
        
        var controller = new MovieController(db);
        var result = await controller.GetMovie(1);
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movie = ok.Value as Movie;
        Assert.That(movie, Is.Not.Null);
        Assert.That(movie.Title, Is.EqualTo("The Shawshank Redemption"));
    }

    [Test]
    public async Task GetMovie_MovieNotFound_ReturnOkAndEmptyObject()
    {
        var db = CreateDb();
        var controller = new MovieController(db);
        var result = await controller.GetMovie(1);
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movie = ok.Value as Movie;
        Assert.That(movie, Is.Null);
    }

    [Test]
    public async Task GetTopFive_6Movies_ReturnOKAndMovieListOrdered()
    {
        var db = CreateDb();
        db.Movies.AddRange(
            new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            },
            new Movie
            {
                Id = 2, ReleaseDate = new DateOnly(1972, 3, 24), Length = 175, Title = "The Godfather",
                Director = "Francis Ford Coppola", Genre = "Crime",
                Description = "The aging patriarch transfers control of his empire."
            },
            new Movie
            {
                Id = 3, ReleaseDate = new DateOnly(2008, 7, 18), Length = 152, Title = "The Dark Knight",
                Director = "Christopher Nolan", Genre = "Action", Description = "Batman faces the Joker in Gotham City."
            },
            new Movie
            {
                Id = 4, ReleaseDate = new DateOnly(1994, 7, 6), Length = 154, Title = "Forrest Gump",
                Director = "Robert Zemeckis", Genre = "Drama", Description = "The life journey of a kind-hearted man."
            },
            new Movie
            {
                Id = 5, ReleaseDate = new DateOnly(2010, 7, 16), Length = 148, Title = "Inception",
                Director = "Christopher Nolan", Genre = "Sci-Fi",
                Description = "A thief enters dreams to steal secrets." 
            },
            new Movie
            {
                Id = 6, ReleaseDate = new DateOnly(1999, 3, 31), Length = 136, Title = "The Matrix",
                Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi",
                Description = "A hacker discovers reality is a simulation."
            }
        );
        
        db.Posts.AddRange(
            new Post { Id = 1, UserId = 1, MovieId = 1, Content = "Absolutely amazing movie." },
            new Post { Id = 2, UserId = 2, MovieId = 1, Content = "One of the best films ever made." },
            new Post { Id = 3, UserId = 3, MovieId = 2, Content = "A true classic." },
            new Post { Id = 4, UserId = 4, MovieId = 2, Content = "Slow but very rewarding." },
            new Post { Id = 5, UserId = 5, MovieId = 2, Content = "Heath Ledger was phenomenal." },
            new Post { Id = 6, UserId = 1, MovieId = 2, Content = "Dark and intense." },
            new Post { Id = 7, UserId = 2, MovieId = 3, Content = "Very emotional." },
            new Post { Id = 8, UserId = 3, MovieId = 3, Content = "Mind-bending." },
            new Post { Id = 9, UserId = 4, MovieId = 3, Content = "Needs multiple watches." },
            new Post { Id = 10, UserId = 5, MovieId = 4, Content = "Changed sci-fi forever." },
            new Post { Id = 11, UserId = 1, MovieId = 5, Content = "Still holds up." },
            new Post { Id = 12, UserId = 2, MovieId = 6, Content = "Very unsettling." }
            );
        await db.SaveChangesAsync();
        
        var controller = new MovieController(db);
        var result = await controller.GetTopFive();
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movies = ok.Value as List<Movie>;
        // Should now have the following order for first 3 movies: 2, 3, 1
        Assert.That(movies[0].Id, Is.EqualTo(2));
        Assert.That(movies[1].Id, Is.EqualTo(3));
        Assert.That(movies[2].Id, Is.EqualTo(1));
        Assert.That(movies.Count, Is.EqualTo(5));
    }

    [Test]
    public async Task GetTopFive_3Movies_ReturnOKAndMovieList()
    {
        var db = CreateDb();
        db.Movies.AddRange(new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            },
            new Movie
            {
                Id = 2, ReleaseDate = new DateOnly(1972, 3, 24), Length = 175, Title = "The Godfather",
                Director = "Francis Ford Coppola", Genre = "Crime",
                Description = "The aging patriarch transfers control of his empire."
            },
            new Movie
            {
                Id = 3, ReleaseDate = new DateOnly(2008, 7, 18), Length = 152, Title = "The Dark Knight",
                Director = "Christopher Nolan", Genre = "Action", Description = "Batman faces the Joker in Gotham City."
            });
        db.Posts.AddRange(
            new Post { Id = 1, UserId = 1, MovieId = 1, Content = "Absolutely amazing movie." },
            new Post { Id = 2, UserId = 2, MovieId = 2, Content = "One of the best films ever made." },
            new Post { Id = 3, UserId = 3, MovieId = 3, Content = "A true classic." }
        );
        
        await db.SaveChangesAsync();
        
        var controller = new MovieController(db);
        var result = await controller.GetTopFive();
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movies = ok.Value as List<Movie>;
        Assert.That(movies.Count, Is.EqualTo(3));
    }

    [Test]
    public async Task GetTopFive_NoMovies_ReturnOKAndEmptyList()
    {
        var db = CreateDb();
        
        var controller = new MovieController(db);
        var result = await controller.GetTopFive();
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var movies = ok.Value as List<Movie>;
        Assert.That(movies.Count, Is.EqualTo(0));
    }
}