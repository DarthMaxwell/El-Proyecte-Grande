using MBW.Server.Controllers;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ServerTests;

public class PostsControllerTests
{
    private static MBDBContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<MBDBContext>()
            .UseInMemoryDatabase(TestContext.CurrentContext.Test.ID)
            .Options;

        return new MBDBContext(options);
    }

    [Test]
    public async Task GetAllPostsForMovie_InvalidMovieId_ReturnOkAndEmptyList()
    {
        var db = CreateDb();
        db.Movies.AddRange(
            new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            }
        );
        db.Posts.AddRange(
            new Post
            {
                Id = 1, Title = "Test Title", Username = "maxwell", MovieId = 1, Content = "Absolutely amazing movie."
            }
        );
        
        await db.SaveChangesAsync();
        var controller = new PostsController(db);
        var result = await controller.GetAllPostsForMovie(2);
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var posts = ok.Value as List<Post>;
        Assert.That(posts.Count, Is.EqualTo(0));
        
    }
    
    [Test]
    public async Task GetAllPostsForMovie_ValidMovieId_ReturnOkAndPostList()
    {
        var db = CreateDb();
        db.Movies.AddRange(
            new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            }
        );
        db.Posts.AddRange(
            new Post
            {
                Id = 1, Title = "Test Title", Username = "simen", MovieId = 1, Content = "Absolutely amazing movie."
            },
            new Post
            {
                Id = 2, Title = "Another Test Title", Username = "admin", MovieId = 1, Content = "One of my favorite movies!"
            }
        );
        await db.SaveChangesAsync();
        var controller = new PostsController(db);
        var result = await controller.GetAllPostsForMovie(1);
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var posts = ok.Value as List<Post>;
        Assert.That(posts, Is.Not.Null);
        Assert.That(posts.Count, Is.EqualTo(2));
    }
    
    [Test]
    public async Task CreatePost_ValidPost_ReturnCreatedAndPost()
    {
        var db = CreateDb();
        db.Movies.AddRange(
            new Movie
            {
                Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption",
                Director = "Frank Darabont", Genre = "Drama",
                Description = "Two imprisoned men bond over a number of years."
            }
        );
        await db.SaveChangesAsync();
        var controller = new PostsController(db);
        var dto = new CreatePostDTO
        {
            MovieId = 1, Title = "New Post", Content = "This is a new post."
        };        

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "testuser")
                }, "TestAuth"))
            }
        };

        ActionResult<Post> result = await controller.CreatePost(dto);

        var created = result.Result as CreatedResult;
        Assert.That(created, Is.Not.Null);

        var returnedPost = created!.Value as Post;
        Assert.That(returnedPost, Is.Not.Null);
        Assert.That(returnedPost!.Title, Is.EqualTo(dto.Title));
        Assert.That(returnedPost.Content, Is.EqualTo(dto.Content));
        Assert.That(returnedPost.MovieId, Is.EqualTo(dto.MovieId));
        Assert.That(returnedPost.Username, Is.EqualTo("testuser"));
    }
    
    //CreatePost_InvalidMovieId_ReturnBadRequest
    
    //UpdatePost_ValidPostUserNotOwner_ReturnUnauthorized
    //UpdatePost_NotValidPostUserNotOwner_ReturnNoContent
    //UpdatePost_NotValidPostUserIsOwner_ReturnNoContent
    //UpdatePost_ValidPostUserIsOwner_ReturnOkayAndPost
    //UpdatePost_ValidPostUserNotOwnerButAdmin_ReturnOkayAndPost
    
    //DeletePost_ValidPostUserNotOwner_ReturnUnauthorized
    //DeletePost_NotValidPostUserNotOwner_ReturnNoContent
    //DeletePost_NotValidPostUserIsOwner_ReturnNoContent
    //DeletePost_ValidPostUserIsOwner_ReturnOkayAndPost
    //DeletePost_ValidPostUserNotOwnerButAdmin_ReturnOkayAndPost
}