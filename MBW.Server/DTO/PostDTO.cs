using System.ComponentModel.DataAnnotations;

namespace MBW.Server.DTO;

public class PostDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Content { get; set; }
}