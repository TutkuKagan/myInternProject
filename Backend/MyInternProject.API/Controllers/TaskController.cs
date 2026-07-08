namespace myInternProject.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myInternProject.API.DTOs;
using myInternProject.API.Services;
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

    public async Task<IActionResult> Create(CreateTaskDTO createTaskDto)
    {
        var newTask = await _taskService.CreateTask(createTaskDto);
       
        return Ok(newTask); 
    }

    public async Task<IActionResult> Update(Guid id, UpdateTaskDTO updateTaskDto)
    {
        var updatedTask = await _taskService.UpdateTask(id, updateTaskDto);

        return Ok(updatedTask);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _taskService.Delete(id);

        return Ok("Task deleted successfully.");
    }


    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetById(id);

        return Ok(task);
    }

    public async Task<IActionResult> FilterListTask(TaskFilterDTO taskFilterDto)
    {
        var tasks = await _taskService.FilterListTask(taskFilterDto);

        return Ok(tasks);
    }




}