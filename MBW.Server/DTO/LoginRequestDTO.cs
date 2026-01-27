using System.ComponentModel.DataAnnotations;

namespace MBW.Server.DTO;

public class LoginRequestDTO
{
    [Required]
    public string Username { get; set; }
    [Required]
    public String Password { get; set; }
}