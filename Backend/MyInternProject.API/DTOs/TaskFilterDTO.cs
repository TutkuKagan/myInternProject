using System.ComponentModel.DataAnnotations;
using myInternProject.API.Models;

namespace myInternProject.API.DTOs;


public class TaskFilterDTO
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public Priority Priority { get; set; } = Priority.Low;
    [Required]
    public Status Status { get; set; } = Status.Pending;


}





