using MBW.Server.Controllers;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServerTests;

public class ReplyControllerTests
{

    private static MBDBContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<MBDBContext>()
            .UseInMemoryDatabase(TestContext.CurrentContext.Test.ID)
            .Options;

        return new MBDBContext(options);
    }

    [Test]
    public async Task GetReplies_InvalidPostId_ReturnOkAndEmptyList()
    {
        var db = CreateDb();
        var controller = new ReplyController(db);
        var result = await controller.GetReplies(1);
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        var replies = ok.Value as List<Reply>;
        Assert.That(replies.Count, Is.EqualTo(0));
    }
    
    [Test]
    public async Task GetReplies_ValidPostId_ReturnOkAndPostList()
    {
        var db = CreateDb();
        db.Posts.AddRange(
            new Post
            {
                Id = 1, Title = "Test Post", Username = "simen", MovieId = 1, Content = "This is a test post."
            }
        );
        db.Replies.AddRange(
            new Reply
            {
                Content = "This is a test reply.", Username = "iver", ParentPostId = 1
            },
            new Reply
            {
                Content = "Another test reply.", Username = "maxwell", ParentPostId = 1
            }
        );
        await db.SaveChangesAsync();
        
        var controller = new ReplyController(db);
        var result = await controller.GetReplies(1);
        
        var ok = result.Result as OkObjectResult;
        Assert.That(ok, Is.Not.Null);
        
        var replies = ok.Value as List<Reply>;
        Assert.That(replies.Count, Is.EqualTo(2));
    }
    
    //CreateReply_ValidPost_ReturnCreatedAndPost
    //CreateReply_InvalidPostId_ReturnBadRequest
    
    //UpdateReply_ValidReplyUserNotOwner_ReturnUnauthorized
    //UpdateReply_NotValidReplyUserNotOwner_ReturnNoContent
    //UpdateReply_NotValidReplyUserIsOwner_ReturnNoContent
    //UpdateReply_ValidReplyUserIsOwner_ReturnOkayAndReply
    //UpdateReply_ValidReplyUserNotOwnerButAdmin_ReturnOkayAndReply
    
    //DeletePost_ValidReplyUserNotOwner_ReturnUnauthorized
    //DeletePost_NotValidReplyUserNotOwner_ReturnNoContent
    //DeletePost_NotValidReplyUserIsOwner_ReturnNoContent
    //DeletePost_ValidReplyUserIsOwner_ReturnOkayAndReply
    //DeletePost_ValidReplyUserNotOwnerButAdmin_ReturnOkayAndReply
}