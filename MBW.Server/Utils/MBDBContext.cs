using MBW.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Utils;

public class MBDBContext : DbContext
{
    public MBDBContext(DbContextOptions<MBDBContext> options) : base(options) { }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    // Add users nad admin
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "The Matrix", Length = 136 },
            new Movie { Id = 2, Title = "Inception", Length = 148 },
            new Movie { Id = 3, Title = "Interstellar", Length = 169 }
        );

        // Seed Posts
        modelBuilder.Entity<Post>().HasData(
            new Post { Id = 1, UserId = 1, MovieId = 1, Content = "First post!" },
            new Post { Id = 2, UserId = 1, MovieId = 2, Content = "Hello World" }
        );

        // Seed Replies
        modelBuilder.Entity<Reply>().HasData(
            new Reply { Id = 3, ParentPostId = 1, Content = "Nice post!" },
            new Reply { Id = 4, ParentPostId = 2, Content = "Welcome!" }
        );
    }
}