using System.ComponentModel.DataAnnotations;

namespace MBW.Server.DTO;

public class CreateReplyDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public string Content { get; set; }

    [Required]
    public int ParentPostId { get; set; }
}