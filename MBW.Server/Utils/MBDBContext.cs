using MBW.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Utils;

public class MBDBContext : DbContext
{
    public MBDBContext(DbContextOptions<MBDBContext> options) : base(options) { }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie(new DateOnly(1994, 10, 14), 142, "The Shawshank Redemption", "Frank Darabont",
        "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
        "Drama"),
        new Movie(new DateOnly(1972, 3, 24), 175, "The Godfather", "Francis Ford Coppola",
        "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
        "Crime"),
         new Movie(new DateOnly(2008, 7, 18), 152, "The Dark Knight", "Christopher Nolan",
        "Batman faces the Joker, a criminal mastermind who plunges Gotham City into chaos.",
        "Action"),
        new Movie(new DateOnly(1994, 7, 6), 154, "Forrest Gump", "Robert Zemeckis",
        "The life journey of a slow-witted but kind-hearted man who witnesses key historical events.",
        "Drama"),
        new Movie(new DateOnly(2010, 7, 16), 148, "Inception", "Christopher Nolan",
        "A thief who steals corporate secrets through dream-sharing technology is given a chance to erase his past crimes.",
        "Sci-Fi"),
        new Movie(new DateOnly(1999, 3, 31), 136, "The Matrix", "Lana Wachowski, Lilly Wachowski",
        "A computer hacker learns about the true nature of reality and his role in the war against its controllers.",
        "Sci-Fi"),
        new Movie(new DateOnly(1999, 10, 15), 139, "Fight Club", "David Fincher",
        "An insomniac office worker forms an underground fight club that spirals out of control.",
        "Drama"),
        new Movie(new DateOnly(2001, 12, 19), 178, "The Lord of the Rings: The Fellowship of the Ring", "Peter Jackson",
        "A hobbit begins a journey to destroy a powerful ring and save Middle-earth.",
        "Fantasy"),
        new Movie(new DateOnly(2002, 12, 18), 179, "The Lord of the Rings: The Two Towers", "Peter Jackson",
        "The fellowship is broken, but the quest to destroy the One Ring continues.",
        "Fantasy"),
        new Movie(new DateOnly(2003, 12, 17), 201, "The Lord of the Rings: The Return of the King", "Peter Jackson",
        "The final confrontation between the forces of good and evil for control of Middle-earth.",
        "Fantasy"),
        new Movie(new DateOnly(1993, 6, 11), 127, "Jurassic Park", "Steven Spielberg",
        "Scientists clone dinosaurs for a theme park that quickly spirals out of control.",
        "Adventure"),
        new Movie(new DateOnly(1977, 5, 25), 121, "Star Wars: A New Hope", "George Lucas",
        "A young farm boy joins a rebellion to defeat an evil galactic empire.",
        "Sci-Fi"),
        new Movie(new DateOnly(1980, 5, 21), 124, "The Empire Strikes Back", "Irvin Kershner",
        "The rebels suffer a major defeat as the Empire closes in.",
        "Sci-Fi"),
        new Movie(new DateOnly(1983, 5, 25), 131, "Return of the Jedi", "Richard Marquand",
        "The final battle between the Rebels and the Empire.",
        "Sci-Fi"),
        new Movie(new DateOnly(1995, 11, 22), 81, "Toy Story", "John Lasseter",
        "A cowboy doll feels threatened when a new spaceman toy becomes his owner's favorite.",
        "Animation"),
        new Movie(new DateOnly(2003, 5, 30), 101, "Finding Nemo", "Andrew Stanton",
        "A clownfish travels across the ocean to rescue his son.",
        "Animation"),
        new Movie(new DateOnly(1997, 12, 19), 195, "Titanic", "James Cameron",
        "A love story unfolds aboard the ill-fated RMS Titanic.",
        "Romance"),
        new Movie(new DateOnly(2014, 11, 7), 169, "Interstellar", "Christopher Nolan",
        "Explorers travel through a wormhole in space to ensure humanity’s survival.",
        "Sci-Fi"),
        new Movie(new DateOnly(1990, 9, 12), 146, "Goodfellas", "Martin Scorsese",
        "The rise and fall of a mob associate in the American Mafia.",
        "Crime"),
        new Movie(new DateOnly(1991, 2, 14), 118, "The Silence of the Lambs", "Jonathan Demme",
        "An FBI trainee seeks help from a cannibalistic serial killer.",
        "Thriller"),
        new Movie(new DateOnly(1998, 7, 24), 169, "Saving Private Ryan", "Steven Spielberg",
        "A group of soldiers search for a paratrooper whose brothers have been killed in action.",
        "War"),
        new Movie(new DateOnly(2000, 5, 5), 155, "Gladiator", "Ridley Scott",
        "A former Roman general seeks revenge against the corrupt emperor.",
        "Action"),
        new Movie(new DateOnly(1985, 7, 3), 116, "Back to the Future", "Robert Zemeckis",
        "A teenager travels back in time and jeopardizes his own existence.",
        "Sci-Fi"),
        new Movie(new DateOnly(1995, 9, 22), 127, "Se7en", "David Fincher",
        "Two detectives hunt a serial killer who uses the seven deadly sins as motives.",
        "Crime"),
        new Movie(new DateOnly(2001, 7, 20), 125, "Harry Potter and the Sorcerer's Stone", "Chris Columbus",
        "A young boy discovers he is a wizard and attends a magical school.",
        "Fantasy"),
        new Movie(new DateOnly(2019, 4, 26), 181, "Avengers: Endgame", "Anthony Russo, Joe Russo",
        "The Avengers assemble one final time to undo the damage caused by Thanos.",
        "Superhero"),
        new Movie(new DateOnly(2008, 6, 13), 126, "Iron Man", "Jon Favreau",
        "A billionaire engineer builds a powered suit to become a superhero.",
        "Superhero"),
        new Movie(new DateOnly(2016, 2, 12), 108, "Deadpool", "Tim Miller",
        "A wisecracking mercenary gains accelerated healing powers.",
        "Action"),
        new Movie(new DateOnly(2017, 3, 3), 137, "Logan", "James Mangold",
        "An aging Wolverine cares for a dying Professor X while protecting a young mutant.",
        "Action"),
        new Movie(new DateOnly(2023, 7, 21), 114, "Oppenheimer", "Christopher Nolan",
        "The story of J. Robert Oppenheimer and the development of the atomic bomb.",
        "Biography")
        );

        // Seed Posts
        modelBuilder.Entity<Post>().HasData(
            new Post(1, 1, "Absolutely amazing movie. The story really stayed with me."),
            new Post(2, 1, "One of the best films ever made. Brilliant performances."),
            new Post(3, 2, "A classic crime movie with unforgettable characters."),
            new Post(4, 2, "The pacing is slow but the payoff is worth it."),
            new Post(5, 3, "Heath Ledger's performance was incredible."),
            new Post(1, 3, "Dark, intense, and very well written."),
            new Post(2, 4, "Heartwarming and emotional. Never gets old."),
            new Post(3, 5, "Mind-bending from start to finish."),
            new Post(4, 5, "You definitely need to watch this more than once."),
            new Post(5, 6, "This movie completely changed how I see sci-fi."),
            new Post(1, 6, "The action and philosophy blend really well."),
            new Post(2, 7, "Disturbing but very well made."),
            new Post(3, 8, "An epic adventure with fantastic world-building."),
            new Post(4, 8, "The music and visuals are outstanding."),
            new Post(5, 11, "Such a fun and thrilling movie."),
            new Post(1, 14, "A perfect ending to the trilogy."),
            new Post(2, 17, "Visually stunning and emotionally powerful."),
            new Post(3, 19, "One of the best gangster movies ever."),
            new Post(4, 21, "Very intense and realistic war scenes."),
            new Post(5, 26, "A satisfying conclusion to a long story arc.")
        );

        // Seed Replies
        modelBuilder.Entity<Reply>().HasData(
            new Reply(2, 1, "I completely agree, this movie really sets the bar high.", 1),

            new Reply(4, 1, "Same here. The performances make it unforgettable.", 2),

            new Reply(1, 2, "That’s true, the characters are what make it so compelling.", 3),

            new Reply(5, 3, "Exactly. It’s dark but incredibly well executed.", 4)
        );
        
        // Seed users
        // TODO
    }
}