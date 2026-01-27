using MBW.Server.Enum;
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
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        base.OnModelCreating(modelBuilder);

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, ReleaseDate = new DateOnly(1994,10,14), Length = 142, Title = "The Shawshank Redemption", Director = "Frank Darabont", Genre = "Drama", Description = "Two imprisoned men bond over a number of years." },
            new Movie { Id = 2, ReleaseDate = new DateOnly(1972,3,24), Length = 175, Title = "The Godfather", Director = "Francis Ford Coppola", Genre = "Crime", Description = "The aging patriarch transfers control of his empire." },
            new Movie { Id = 3, ReleaseDate = new DateOnly(2008,7,18), Length = 152, Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Description = "Batman faces the Joker in Gotham City." },
            new Movie { Id = 4, ReleaseDate = new DateOnly(1994,7,6), Length = 154, Title = "Forrest Gump", Director = "Robert Zemeckis", Genre = "Drama", Description = "The life journey of a kind-hearted man." },
            new Movie { Id = 5, ReleaseDate = new DateOnly(2010,7,16), Length = 148, Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Description = "A thief enters dreams to steal secrets." },
            new Movie { Id = 6, ReleaseDate = new DateOnly(1999,3,31), Length = 136, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi", Description = "A hacker discovers reality is a simulation." },
            new Movie { Id = 7, ReleaseDate = new DateOnly(1999,10,15), Length = 139, Title = "Fight Club", Director = "David Fincher", Genre = "Drama", Description = "An underground fight club spirals out of control." },
            new Movie { Id = 8, ReleaseDate = new DateOnly(2001,12,19), Length = 178, Title = "LOTR: Fellowship of the Ring", Director = "Peter Jackson", Genre = "Fantasy", Description = "A hobbit begins a journey to destroy a ring." },
            new Movie { Id = 9, ReleaseDate = new DateOnly(2002,12,18), Length = 179, Title = "LOTR: The Two Towers", Director = "Peter Jackson", Genre = "Fantasy", Description = "The quest continues as darkness spreads." },
            new Movie { Id = 10, ReleaseDate = new DateOnly(2003,12,17), Length = 201, Title = "LOTR: Return of the King", Director = "Peter Jackson", Genre = "Fantasy", Description = "The final battle for Middle-earth." },
            new Movie { Id = 11, ReleaseDate = new DateOnly(1993,6,11), Length = 127, Title = "Jurassic Park", Director = "Steven Spielberg", Genre = "Adventure", Description = "Dinosaurs are brought back to life." },
            new Movie { Id = 12, ReleaseDate = new DateOnly(1977,5,25), Length = 121, Title = "Star Wars: A New Hope", Director = "George Lucas", Genre = "Sci-Fi", Description = "A young hero joins a galactic rebellion." },
            new Movie { Id = 13, ReleaseDate = new DateOnly(1980,5,21), Length = 124, Title = "The Empire Strikes Back", Director = "Irvin Kershner", Genre = "Sci-Fi", Description = "The Empire strikes back against the rebels." },
            new Movie { Id = 14, ReleaseDate = new DateOnly(1983,5,25), Length = 131, Title = "Return of the Jedi", Director = "Richard Marquand", Genre = "Sci-Fi", Description = "The final showdown with the Empire." },
            new Movie { Id = 15, ReleaseDate = new DateOnly(1995,11,22), Length = 81, Title = "Toy Story", Director = "John Lasseter", Genre = "Animation", Description = "Toys come to life when humans aren’t around." },
            new Movie { Id = 16, ReleaseDate = new DateOnly(2003,5,30), Length = 101, Title = "Finding Nemo", Director = "Andrew Stanton", Genre = "Animation", Description = "A clownfish searches for his son." },
            new Movie { Id = 17, ReleaseDate = new DateOnly(1997,12,19), Length = 195, Title = "Titanic", Director = "James Cameron", Genre = "Romance", Description = "A love story aboard the Titanic." },
            new Movie { Id = 18, ReleaseDate = new DateOnly(2014,11,7), Length = 169, Title = "Interstellar", Director = "Christopher Nolan", Genre = "Sci-Fi", Description = "Humanity searches for a new home." },
            new Movie { Id = 19, ReleaseDate = new DateOnly(1990,9,12), Length = 146, Title = "Goodfellas", Director = "Martin Scorsese", Genre = "Crime", Description = "The rise and fall of a mobster." },
            new Movie { Id = 20, ReleaseDate = new DateOnly(1991,2,14), Length = 118, Title = "The Silence of the Lambs", Director = "Jonathan Demme", Genre = "Thriller", Description = "An FBI trainee hunts a serial killer." },
            new Movie { Id = 21, ReleaseDate = new DateOnly(1998,7,24), Length = 169, Title = "Saving Private Ryan", Director = "Steven Spielberg", Genre = "War", Description = "A mission to save a soldier during WWII." },
            new Movie { Id = 22, ReleaseDate = new DateOnly(2000,5,5), Length = 155, Title = "Gladiator", Director = "Ridley Scott", Genre = "Action", Description = "A Roman general seeks revenge." },
            new Movie { Id = 23, ReleaseDate = new DateOnly(1985,7,3), Length = 116, Title = "Back to the Future", Director = "Robert Zemeckis", Genre = "Sci-Fi", Description = "A teenager travels through time." },
            new Movie { Id = 24, ReleaseDate = new DateOnly(1995,9,22), Length = 127, Title = "Se7en", Director = "David Fincher", Genre = "Crime", Description = "Detectives hunt a serial killer." },
            new Movie { Id = 25, ReleaseDate = new DateOnly(2001,7,20), Length = 125, Title = "Harry Potter and the Sorcerer's Stone", Director = "Chris Columbus", Genre = "Fantasy", Description = "A boy discovers he is a wizard." },
            new Movie { Id = 26, ReleaseDate = new DateOnly(2019,4,26), Length = 181, Title = "Avengers: Endgame", Director = "Russo Brothers", Genre = "Superhero", Description = "The Avengers fight Thanos one last time." },
            new Movie { Id = 27, ReleaseDate = new DateOnly(2008,6,13), Length = 126, Title = "Iron Man", Director = "Jon Favreau", Genre = "Superhero", Description = "Tony Stark becomes Iron Man." },
            new Movie { Id = 28, ReleaseDate = new DateOnly(2016,2,12), Length = 108, Title = "Deadpool", Director = "Tim Miller", Genre = "Action", Description = "A mercenary becomes a wisecracking hero." },
            new Movie { Id = 29, ReleaseDate = new DateOnly(2017,3,3), Length = 137, Title = "Logan", Director = "James Mangold", Genre = "Action", Description = "An aging Wolverine protects a young mutant." },
            new Movie { Id = 30, ReleaseDate = new DateOnly(2023,7,21), Length = 114, Title = "Oppenheimer", Director = "Christopher Nolan", Genre = "Biography", Description = "The story of the atomic bomb." }
        );

        // Seed Posts
        modelBuilder.Entity<Post>().HasData(
            new Post { Id = 1, UserId = 1, MovieId = 1, Content = "Absolutely amazing movie." },
            new Post { Id = 2, UserId = 2, MovieId = 1, Content = "One of the best films ever made." },
            new Post { Id = 3, UserId = 3, MovieId = 2, Content = "A true classic." },
            new Post { Id = 4, UserId = 4, MovieId = 2, Content = "Slow but very rewarding." },
            new Post { Id = 5, UserId = 5, MovieId = 3, Content = "Heath Ledger was phenomenal." },
            new Post { Id = 6, UserId = 1, MovieId = 3, Content = "Dark and intense." },
            new Post { Id = 7, UserId = 2, MovieId = 4, Content = "Very emotional." },
            new Post { Id = 8, UserId = 3, MovieId = 5, Content = "Mind-bending." },
            new Post { Id = 9, UserId = 4, MovieId = 5, Content = "Needs multiple watches." },
            new Post { Id = 10, UserId = 5, MovieId = 6, Content = "Changed sci-fi forever." },
            new Post { Id = 11, UserId = 1, MovieId = 6, Content = "Still holds up." },
            new Post { Id = 12, UserId = 2, MovieId = 7, Content = "Very unsettling." },
            new Post { Id = 13, UserId = 3, MovieId = 8, Content = "Epic fantasy." },
            new Post { Id = 14, UserId = 4, MovieId = 8, Content = "Amazing soundtrack." },
            new Post { Id = 15, UserId = 5, MovieId = 11, Content = "So much fun." },
            new Post { Id = 16, UserId = 1, MovieId = 14, Content = "Perfect ending." },
            new Post { Id = 17, UserId = 2, MovieId = 17, Content = "Visually stunning." },
            new Post { Id = 18, UserId = 3, MovieId = 19, Content = "Classic Scorsese." },
            new Post { Id = 19, UserId = 4, MovieId = 21, Content = "Very intense war scenes." },
            new Post { Id = 20, UserId = 5, MovieId = 26, Content = "A satisfying conclusion." }
        );

        // Seed Replies
        modelBuilder.Entity<Reply>().HasData(
            new Reply { Id = 21, UserId = 2, Content = "Totally agree.", ParentPostId = 1 },
            new Reply { Id = 22, UserId = 4, Content = "Same here!", ParentPostId = 2 },
            new Reply { Id = 23, UserId = 1, Content = "Characters are amazing.", ParentPostId = 3 },
            new Reply { Id = 24, UserId = 5, Content = "Perfectly executed.", ParentPostId = 4 }
        );
        
        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1, 
                Name = "maxwell", 
                Hash = "OQwI3mQAJmIFxBEeE9MzXiu3XINgggUUM4Esx5gPPtU/pDcAm63eNuzB6+dW57C4JYDFjri7sgp0z7uEqkvOGw==",
                Salt = "6FtjrN1yJf/ai3tP40hQFICgClscl5oxQIBEehyJXckca2cgT4K41KJAWUJ92gj4U7kmJSmS6sj8yWRcdT/79Ub3bsHsiYGzAIng4+7MRVVJIqpHu9ZU4ieHQGMteh4zmAsXJ/4kKRnB6IW8v7rpW+lPzZ9ozgXY9Kz4+Wtw1Us=",
                Role = Roles.ADMIN
            },
            new User 
            { 
                Id = 2, 
                Name = "iver", 
                Hash = "OQwI3mQAJmIFxBEeE9MzXiu3XINgggUUM4Esx5gPPtU/pDcAm63eNuzB6+dW57C4JYDFjri7sgp0z7uEqkvOGw==",
                Salt = "6FtjrN1yJf/ai3tP40hQFICgClscl5oxQIBEehyJXckca2cgT4K41KJAWUJ92gj4U7kmJSmS6sj8yWRcdT/79Ub3bsHsiYGzAIng4+7MRVVJIqpHu9ZU4ieHQGMteh4zmAsXJ/4kKRnB6IW8v7rpW+lPzZ9ozgXY9Kz4+Wtw1Us=",
                Role = Roles.ADMIN
            },
            new User 
            { 
                Id = 3, 
                Name = "simen", 
                Hash = "OQwI3mQAJmIFxBEeE9MzXiu3XINgggUUM4Esx5gPPtU/pDcAm63eNuzB6+dW57C4JYDFjri7sgp0z7uEqkvOGw==",
                Salt = "6FtjrN1yJf/ai3tP40hQFICgClscl5oxQIBEehyJXckca2cgT4K41KJAWUJ92gj4U7kmJSmS6sj8yWRcdT/79Ub3bsHsiYGzAIng4+7MRVVJIqpHu9ZU4ieHQGMteh4zmAsXJ/4kKRnB6IW8v7rpW+lPzZ9ozgXY9Kz4+Wtw1Us=",
                Role = Roles.AUTHENTICATEDUSER
            }
        );
    }
}