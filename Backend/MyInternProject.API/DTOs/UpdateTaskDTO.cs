using MyInternProject.API.Models;

namespace MyInternProject.API.DTOs;
using System.ComponentModel.DataAnnotations;


public class UpdateTaskDTO
{
    [Required,MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required,MaxLength(200)]
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public DateTime DueDate { get; set; } 
    public DateTime CompletedAt { get; set; } 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



}




