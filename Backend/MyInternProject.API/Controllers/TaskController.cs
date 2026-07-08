namespace MyInternProject.API.Controllers;
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
        var newTask = await _taskService.CreateTask(createTaskDto);
       
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



    [HttpGet("filter List")]
    public async Task<IActionResult> FilterListTask(TaskFilterDTO taskFilterDto)
    {
        var tasks = await _taskService.FilterListTask(taskFilterDto);

        return Ok(tasks);
    }

}