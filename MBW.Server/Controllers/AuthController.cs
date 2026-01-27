using System.Data.Common;
using MBW.Server.DTO;
using MBW.Server.Models;
using MBW.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JWTService _jwtService;
    private readonly MBDBContext _dbContext;

    public AuthController(JWTService jwtService, MBDBContext context)
    {
        _jwtService = jwtService;
        _dbContext = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
    {
        User? user = null;
        
        try
        {
            user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Username);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }

        if (user == null)
        {
            return Unauthorized("Invalid credentials"); // 401 Unauthorized
        }

        if (!PasswordUtil.VerifyPasswordHash(request.Password, user.Hash, user.Salt))
        {
            return Unauthorized("Invalid credentials"); // 401 Unauthorized
        }
        
        string token = _jwtService.GenerateToken(user);
        
        return Ok(new LoginResponseDTO
        {
            Token = token,
            Username = user.Name,
            Role = user.Role
        }); // 200 Ok
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(LoginRequestDTO request)
    {
        try
        {
            if (await _dbContext.Users.AnyAsync(u => u.Name == request.Username))
            {
                return BadRequest("Username already exists"); // 400 Bad Request
            }
            
            PasswordUtil.CreatePasswordHash(request.Password, out string hash, out string salt);
            
            User user = new User(request.Username, salt, hash);
            
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            
            return Created("", new
            {
                id = user.Id,
                username = user.Name,
                role = user.Role
            }); // 201 Created
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
}