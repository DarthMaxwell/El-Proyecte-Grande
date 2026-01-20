using System.ComponentModel.DataAnnotations;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MBW.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReplyController : ControllerBase
{
    /*private readonly MBDBContext _dbContext;
    
    public  ReplyController(MBDBContext dbContext)
    {
        _dbContext = dbContext;
    }*/
    
    // DUMMY DATA
    List<Reply> replies = new List<Reply>
    {
        new Reply(
            userId: 2,
            moveId: 101,
            content: "I agree — this move surprised me too.",
            parentPostId: 10
        ),
        new Reply(
            userId: 3,
            moveId: 101,
            content: "Same here, I started using it after seeing this post.",
            parentPostId: 10
        ),
        new Reply(
            userId: 4,
            moveId: 101,
            content: "I had mixed results, but it’s definitely situational.",
            parentPostId: 10
        ),
        new Reply(
            userId: 5,
            moveId: 101,
            content: "Good point — positioning makes a huge difference.",
            parentPostId: 10
        ),
        new Reply(
            userId: 6,
            moveId: 101,
            content: "Thanks for the breakdown, this helped a lot.",
            parentPostId: 10
        )
    };
    
    // GET: api/reply/{postId}
    [HttpGet("{replyId}")]
    public async Task<ActionResult<List<Reply>>> GetReplies(int replyId)
    {
        var result = replies.AsEnumerable().Where(r => r.ParentPostId == replyId).ToList();
        //await _dbContext.Replies.Where(r => r.ParentPostId == postId);
        
        return Ok(result); // 200 Ok
    }
    
    // POST: api/reply
    // AUTHENTICATED USER
    [HttpPost]
    public async Task<ActionResult<Reply>> CreateReply(CreateReplyDTO createReply)
    {
        Reply r = new Reply(createReply.UserId, createReply.MoveId, createReply.Content, createReply.ParentPostId);
        
        //_dbContext.Replies.Add(r);
        //await _dbContext.SaveChangesAsync();
        
        replies.Add(r);
        
        
        return CreatedAtAction(
            nameof(GetReplies),
            new { postId = r.ParentPostId },
            r
        ); // 201 Created
    }
    
    // PUT: api/reply
    // AUTHENTICATED USER
    [HttpPut]
    public async Task<ActionResult<Reply>> UpdateReply(ReplyDTO reply)
    {
        // NEEDS USER VALIDATION
        Reply? res = replies.FirstOrDefault(r => r.Id == reply.Id); // CHANGE TO DATABASE

        if (res == null)
            return NotFound($"Reply with ID {reply.Id} not found."); // 404 Not Found

        res.Content = reply.Content; // CHANGE TO DATABASE
        
        return Ok(res); // 200 Ok
    }
    
    // DELETE: api/reply/{replyId}
    // AUTHENTICATED USER
    [HttpDelete("{replyId}")]
    public async Task<ActionResult> DeleteReply(int replyId)
    {
        // NEEDS USER VALIDATION
        Reply? res = replies.FirstOrDefault(r => r.Id == replyId); // CHANGE TO DATABASE
        
        if (res == null)
            return NotFound($"Reply with ID {replyId} not found."); // 404 Not Found
        
        replies.Remove(res); // CHANGE TO DATABASE 
        
        return NoContent(); // 204 No Content
    }
}