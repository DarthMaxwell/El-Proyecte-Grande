namespace MBW.Server.Models;

public class Movie
{
    public int Id { get; set; }
    public DateOnly ReleaseDate { get; set; }
    
    public int Length { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    
    public Movie() {}

    public Movie(DateOnly releaseDate, int length, string title, string director, string description, string genre)
    {
        ReleaseDate = releaseDate;
        Length = length;
        Title = title;
        Director = director;
        Description = description;
        Genre = genre;
    }
}