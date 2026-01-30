namespace MBW.Server.Models;

public class Post
{
    public int Id { get; set; } 
    public int MovieId { get; set; }
    public string Username { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Post() {}

    public Post(int movieId, string username, string title, string content)
    {
        MovieId = movieId;
        Username = username;
        Title = title;
        Content = content;
    }
}