using MyInternProject.API.Models;

namespace MyInternProject.API.DTOs;

public class TaskQueryDTO
{

    public string? SearchTerm { get; set; } 
    public Priority? Priority { get; set; }
    public Status? Status { get; set; }  
    public string? CategoryName { get; set; } 
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10; 
}