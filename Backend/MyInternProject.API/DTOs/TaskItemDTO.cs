using MyInternProject.API.Models;

namespace MyInternProject.API.DTOs;


public class TaskItemDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public DateTime DueDate { get; set; }
    public DateTime CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


}





