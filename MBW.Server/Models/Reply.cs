namespace MBW.Server.Models;

public class Reply
{
    public int Id { get; set; } 
    public int ParentPostId { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    
    public Reply() {}

    public Reply(int parentPostId, string username, string content)
    {
        ParentPostId = parentPostId;
        Username = username;
        Content = content;
    }
}