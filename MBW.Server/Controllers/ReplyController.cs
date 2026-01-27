using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Claims;
using MBW.Server.DTO;
using MBW.Server.Enum;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Authorization;
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
        try
        {
            var res = await _dbContext.Replies.Where(r => r.ParentPostId == postId).ToListAsync();
            
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // POST: api/reply
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Reply>> CreateReply(CreateReplyDTO createReply)
    {
        try
        {
            User u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Reply r = new Reply(createReply.ParentPostId, u.Id, createReply.Content);
            
            await _dbContext.Replies.AddAsync(r);
            await _dbContext.SaveChangesAsync();
            
            return Created(
                $"/api/reply/{r.ParentPostId}",
                r
            );
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // PUT: api/reply
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Reply>> UpdateReply(ReplyDTO reply)
    {
        try
        {
            User? u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Reply? res = _dbContext.Replies.FirstOrDefault(r => r.Id == reply.Id);

            if (res == null)
                return NotFound();
            
            if (u == null || (u.Id != res.UserId && u.Role != Roles.ADMIN))
                return Unauthorized("This is not your post");

            res.Content = reply.Content;
            _dbContext.Replies.Update(res);
            await _dbContext.SaveChangesAsync();
            
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
    
    // DELETE: api/reply/{replyId}
    [Authorize]
    [HttpDelete("{replyId}")]
    public async Task<ActionResult> DeleteReply(int replyId)
    {
        try
        {
            User? u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirst(ClaimTypes.Name).Value);
            Reply? res = _dbContext.Replies.FirstOrDefault(r => r.Id == replyId);

            if (res == null)
                return NotFound();
            
            if (u == null || (u.Id != res.UserId && u.Role != Roles.ADMIN))
                return Unauthorized("This is not your post");
            
            _dbContext.Replies.Remove(res);
            await _dbContext.SaveChangesAsync();
            
            return Ok(res);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
}   