using System.ComponentModel.DataAnnotations;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReplyController : ControllerBase
{
    private readonly MBDBContext _dbContext;
    
    public  ReplyController(MBDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // GET: api/reply/{postId}
    [HttpGet("{postId}")]
    public async Task<ActionResult<List<Reply>>> GetReplies(int postId)
    {
        var result = await _dbContext.Replies.Where(r => r.ParentPostId == postId).ToListAsync().ConfigureAwait(false);
        
        return Ok(result); // 200 Ok
    }
    
    // POST: api/reply
    // AUTHENTICATED USER
    [HttpPost]
    public async Task<ActionResult<Reply>> CreateReply(CreateReplyDTO createReply)
    {
        Reply r = new Reply(createReply.UserId, createReply.MoveId, createReply.Content, createReply.ParentPostId);
        
        _dbContext.Replies.Add(r);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        
        return Created(
            $"/api/reply/{r.ParentPostId}",
            r
        ); // 201 Created
    }
    
    // PUT: api/reply
    // AUTHENTICATED USER
    [HttpPut]
    public async Task<ActionResult<Reply>> UpdateReply(ReplyDTO reply)
    {
        // NEEDS USER VALIDATION
        Reply? res = _dbContext.Replies.FirstOrDefault(r => r.Id == reply.Id);

        if (res == null)
            return NoContent(); // 204 No Content

        res.Content = reply.Content;
        _dbContext.Replies.Update(res);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        
        return Ok(res); // 200 Ok
    }
    
    // DELETE: api/reply/{replyId}
    // AUTHENTICATED USER
    [HttpDelete("{replyId}")]
    public async Task<ActionResult> DeleteReply(int replyId)
    {
        // NEEDS USER VALIDATION
        Reply? res = _dbContext.Replies.FirstOrDefault(r => r.Id == replyId);
        
        if (res == null)
            return NoContent(); // 204 No Content
        
        _dbContext.Replies.Remove(res);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        
        return Ok(res); // 200 Ok
    }
}