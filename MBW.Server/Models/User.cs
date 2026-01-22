using MBW.Server.Enum;

namespace MBW.Server.Models;

public abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Salt { get; set; }
    public string Hash { get; set; }
    public Roles Role { get; set; } = Roles.AUTHENTICATEDUSER;

    protected User(string name, string salt, string hash)
    {
        Name = name;
        Salt = salt;
        Hash = hash;
    }
}