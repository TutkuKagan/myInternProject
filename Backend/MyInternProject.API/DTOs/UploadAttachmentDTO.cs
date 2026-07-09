using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyInternProject.API.DTOs;

public class UploadAttachmentDTO
{
    [Required]
    public Guid TaskItemId { get; set; } 

    [Required]
    public IFormFile File { get; set; } = null!;
}