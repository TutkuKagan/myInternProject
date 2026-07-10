using System.ComponentModel.DataAnnotations;

namespace MyInternProject.API.DTOs;

public class CreateCommentDTO
{
    [Required]
    public Guid TaskId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Comment { get; set; } = string.Empty;
}