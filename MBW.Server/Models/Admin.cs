namespace MBW.Server.Models;

public class Admin : User
{
    public Admin(string name, string salt, string hash) : base(name, salt, hash)
    {
        
    }
}