using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using MBW.Server.Controllers;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

        // Check DB
        var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MBDBContext>();

        var user = await db.Users.FirstOrDefaultAsync(u => u.Name == "newuser");

        Assert.That(user, Is.Not.Null);
    }


    //Login_UserValidPassword_ReturnOkAndLoginResponse
    //Login_UserNotValidPassword_ReturnUnauthorized
}