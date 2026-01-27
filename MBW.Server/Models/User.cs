using System.ComponentModel.DataAnnotations;
using MBW.Server.Enum;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Models;

[Index(nameof(Name), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public string Salt { get; set; }
    [Required]
    public string Hash { get; set; }
    [Required]
    public Roles Role { get; set; } = Roles.AUTHENTICATEDUSER;

    public User() { }

    public User(string name, string salt, string hash)
    {
        Name = name;
        Salt = salt;
        Hash = hash;
    }
}