namespace MBW.Server.Models;

public class AuthenticatedUser : User
{
    public AuthenticatedUser(string name, string salt, string hash) : base(name, salt, hash)
    {
        
    }
}