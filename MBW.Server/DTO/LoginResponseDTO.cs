using MBW.Server.Enum;

namespace MBW.Server.DTO;

public class LoginResponseDTO
{
    public string Token { get; set; }
    public string Username { get; set; }
    public Roles Role { get; set; }
}