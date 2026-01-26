using System.Security.Cryptography;

namespace MBW.Server.Utils;

public class PasswordUtil
{
    public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }
    }

    public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        using (HMACSHA512 hmac = new HMACSHA512(saltBytes))
        {
            string computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}