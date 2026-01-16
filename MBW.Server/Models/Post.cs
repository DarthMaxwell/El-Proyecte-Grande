namespace MBW.Server.Models;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MoveId { get; set; }
    public string Content { get; set; }

    public Post(int userId, int moveId, string content)
    {
        UserId = userId;
        MoveId = moveId;
        Content = content;
    }
}