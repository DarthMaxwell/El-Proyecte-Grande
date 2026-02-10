using MBW.Server.Controllers;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServerTests;

public class PostsControllerTests
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
    
    //GetAllPostsForMovie_ValidMovieId_ReturnOkAndPostList
    
    //GetAllPostsForUser_ReturnOkAndPostList
    //GetAllPostsForUser_NoUserPosts_ReturnsOkAndEmptyList
    
    //CreatePost_ValidPost_ReturnCreatedAndPost
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