using MBW.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Utils;

public class MBDBContext : DbContext
{
    public MBDBContext(DbContextOptions<MBDBContext> options) : base(options) { }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    // will need users and admins?
}