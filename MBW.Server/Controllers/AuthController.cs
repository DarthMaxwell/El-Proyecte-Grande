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
        User? u = null;
        
        try
        {
            u = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == request.Username);
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }

        if (u == null || !PasswordUtil.VerifyPasswordHash(request.Password, u.Hash, u.Salt))
        {
            return Unauthorized("Invalid credentials");
        }
        
        string token = _jwtService.GenerateToken(u);
        
        return Ok(new LoginResponseDTO
        {
            Token = token,
            Username = u.Name,
            Role = u.Role
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(LoginRequestDTO request)
    {
        try
        {
            if (await _dbContext.Users.AnyAsync(u => u.Name == request.Username))
            {
                return BadRequest("Username already exists");
            }
            
            PasswordUtil.CreatePasswordHash(request.Password, out string hash, out string salt);
            
            User u = new User(request.Username, salt, hash);
            
            await _dbContext.Users.AddAsync(u);
            await _dbContext.SaveChangesAsync();
            
            return Created("", new
            {
                id = u.Id,
                username = u.Name,
                role = u.Role
            });
        }
        catch (DbException)
        {
            return StatusCode(503, "Database unavailable.");
        }
    }
}