namespace MyInternProject.API.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInternProject.API.DTOs;
using MyInternProject.API.Services;



[Authorize]
[ApiController] 
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
            _taskService = taskService;
    }



    [HttpPost("createTask")]
    public async Task<IActionResult> Create(CreateTaskDTO createTaskDto)
    {   
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                      ?? User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
    {
        return Unauthorized("User not found via token");
    }

    var userId = Guid.Parse(userIdClaim);
    var newTask = await _taskService.CreateTask(createTaskDto, userId);
       
    return Ok(newTask);
    }



    [HttpPut("updateTask")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskDTO updateTaskDto)
    {
        var updatedTask = await _taskService.UpdateTask(id, updateTaskDto);

        return Ok(updatedTask);
    }



    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _taskService.Delete(id);

        return Ok("Task deleted successfully.");
    }



    [HttpGet("getById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetById(id);

        return Ok(task);
    }



    [HttpGet("filterList")]
    public async Task<IActionResult> FilterListTask(TaskFilterDTO taskFilterDto)
    {
        var tasks = await _taskService.FilterListTask(taskFilterDto);

        return Ok(tasks);
    }


    [HttpGet("search")] 
    public async Task<IActionResult> GetSearch([FromQuery] TaskQueryDTO queryDto)
    {
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User claim not found in token");
        }

        var userId = Guid.Parse(userIdClaim);
        var tasks = await _taskService.GetFilteredTasksAsync(queryDto, userId);
        return Ok(tasks);
    }


    [HttpPost("upload-attachment")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAttachment([FromForm] UploadAttachmentDTO uploadDto)
    {
    
    var attachment = await _taskService.UploadAttachmentAsync(uploadDto);
    return Ok(attachment);
    }

}