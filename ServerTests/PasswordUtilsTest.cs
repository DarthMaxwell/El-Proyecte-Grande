using MBW.Server.Utils;

namespace ServerTests;

public class PasswordUtilsTest
{
    [Test]
    public void CreatePasswordHash_ShouldGenerateHashAndSalt()
    {
        PasswordUtil.CreatePasswordHash("testing",  out string hash, out string salt);
        
        Assert.That(hash, Is.Not.Null);
        Assert.That(salt, Is.Not.Null);
        Assert.That(hash, Is.Not.Empty);
        Assert.That(salt, Is.Not.Empty);
    }

    [Test]
    public void CreatePasswordHash_ShouldGenerateUniqueSaltsAndHashes()
    {
        PasswordUtil.CreatePasswordHash("testinghgdh5h3", out string hash, out string salt);
        PasswordUtil.CreatePasswordHash("testinghgdh5h3", out string hash2,  out string salt2);
        
        Assert.That(salt, Is.Not.EqualTo(salt2));
        Assert.That(hash, Is.Not.EqualTo(hash2));
    }

    [Test]
    public void VerifyPasswordHash_ShouldReturnTrue_WhenPasswordMatches()
    {
        PasswordUtil.CreatePasswordHash("testing123", out string hash, out string salt);
        
        Assert.That(PasswordUtil.VerifyPasswordHash("testing123", hash, salt), Is.True);
    }

    [Test]
    public void VerifyPasswordHash_ShouldReturnFalse_WhenSaltIsModified()
    {
        PasswordUtil.CreatePasswordHash("tesdfsggg4s2", out string hash, out string salt);
        PasswordUtil.CreatePasswordHash("tesdfsggg4ssda2", out string hash2, out string salt2);
        
        Assert.That(PasswordUtil.VerifyPasswordHash("tesdfsggg4s2", hash, salt2), Is.False);
    }
}