using System.ComponentModel.DataAnnotations;

namespace MBW.Server.DTO;

public class CreatePostDTO
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Content { get; set; }
}