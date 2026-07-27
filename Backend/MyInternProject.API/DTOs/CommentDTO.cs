namespace MyInternProject.API.DTOs;

public class CommentDTO
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}