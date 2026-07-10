namespace MyInternProject.API.DTOs;

public class TaskStatisticsDTO
{
    public int TotalTasks { get; set; }
    public int PendingTasks { get; set; }
    public int InProgressTasks { get; set; }
    public int CompletedTasks { get; set; }
}