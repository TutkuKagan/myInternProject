using myInternProject.API.Models;

namespace myInternProject.API.DTOs;


public class CreateTaskDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public DateTime DueDate { get; set; }


}




