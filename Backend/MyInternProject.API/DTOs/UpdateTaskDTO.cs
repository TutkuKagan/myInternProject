using myInternProject.API.Models;

namespace myInternProject.API.DTOs;


public class UpdateTaskDTO
{
        public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public DateTime DueDate { get; set; } 
    public DateTime CompletedAt { get; set; } 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



}




