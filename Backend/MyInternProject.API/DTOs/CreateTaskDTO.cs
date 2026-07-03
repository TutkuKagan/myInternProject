using System.ComponentModel.DataAnnotations;
using myInternProject.API.Models;

namespace myInternProject.API.DTOs;


public class CreateTaskDTO
{
    [Required,MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public Priority Priority { get; set; } = Priority.Low;
    public DateTime DueDate { get; set; }


}




