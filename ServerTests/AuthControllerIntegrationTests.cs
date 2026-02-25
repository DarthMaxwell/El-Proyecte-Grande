using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ServerTests;

public class AuthControllerIntegrationTests
{
    private WebApplicationFactory<Program> CreateFactory()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
    }
    
    [Test]
    public async Task Register_ValidUsernameAndPassword_ReturnCreated() 
    {
        var factory = CreateFactory();
        var client = factory.CreateClient();

        var dto = new LoginRequestDTO
        {
            Username = "newuser",
            Password = "newpassword"
        };

        var response = await client.PostAsJsonAsync("/api/auth/register", dto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

        var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MBDBContext>();

        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == "newuser");

        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public async Task Login_UserValidPassword_ReturnOkAndLoginResponse()
    {
        var factory = CreateFactory();
        var client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MBDBContext>();

        var password = "pwd123";
        PasswordUtil.CreatePasswordHash(password, out string hash, out string salt);

        db.Users.Add(new User
        {
            Name = "testuser",
            Hash = hash,
            Salt = salt,
        });
        await db.SaveChangesAsync();

        var dto = new LoginRequestDTO
        {
            Username = "testuser",
            Password = password
        };

        var response = await client.PostAsJsonAsync("/api/auth/login", dto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

        Assert.That(loginResponse, Is.Not.Null);
        Assert.That(loginResponse.Username, Is.EqualTo("testuser"));
    }

    [Test]
    public async Task Login_UserNotValidPassword_ReturnUnauthorized()
    {
        var factory = CreateFactory();
        var client = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MBDBContext>();

        var password = "movie123";
        PasswordUtil.CreatePasswordHash(password, out string hash, out string salt);

        db.Users.Add(new User
        {
            Name = "ilovemovies",
            Hash = hash,
            Salt = salt,
        });
        await db.SaveChangesAsync();

        var dto = new LoginRequestDTO
        {
            Username = "ilovemovies",
            Password = "movie321"
        };

        var response = await client.PostAsJsonAsync("/api/auth/login", dto);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
}