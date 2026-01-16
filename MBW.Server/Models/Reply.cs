namespace MBW.Server.Models;

public class Reply : Post
{
    public int ParentPostId { get; set; }
    
    public Reply(int userId, int moveId, string content, int parentPostId) : base(userId, moveId, content)
    {
        ParentPostId = parentPostId;
    }
}