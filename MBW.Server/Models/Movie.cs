namespace MBW.Server.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Length { get; set; }
    
    public Movie() {}
    
    public Movie(string title, int length)
    {
        Title = title;
        Length = length;
    }
}