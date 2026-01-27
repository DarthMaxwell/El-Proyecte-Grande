namespace MBW.Server.Models;

public class Reply
{
    public int Id { get; set; } 
    public int ParentPostId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    
    public Reply() {}

    public Reply(int parentPostId, int userId, string content)
    {
        ParentPostId = parentPostId;
        UserId = userId;
        Content = content;
    }
}