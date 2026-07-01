namespace myInternProject.API.Models;

public class TaskItem
{
    public int  Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public DateTime DueDate { get; set; }
    public DateTime CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}