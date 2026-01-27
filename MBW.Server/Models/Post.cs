namespace MBW.Server.Models;

public class Post
{
    public int Id { get; set; } 
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string Content { get; set; }

    public Post() {}
    
    public Post(int userId, int movieId, string content)
    {
        UserId = userId;
        MovieId = movieId;
        Content = content;
    }
}