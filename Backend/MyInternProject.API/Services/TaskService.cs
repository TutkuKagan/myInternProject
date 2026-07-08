using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;


public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TaskService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<TaskItemDTO> CreateTask (CreateTaskDTO createTaskDto)
    {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Title == createTaskDto.Title);
                if(existingTask != null)
                      {
                     throw new Exception("This task is already included.");
                          }             

            var taskEntity = _mapper.Map<TaskItem>(createTaskDto);
            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskItemDTO>(taskEntity);
        
    }

    public async Task<TaskItemDTO> UpdateTask (Guid id ,UpdateTaskDTO updateTaskDto)
    {

        var notUpdatedTask = await _context.Tasks.FindAsync(id);
        if (notUpdatedTask == null)
        {
            throw new Exception("There is no such Task.");
        }
        var UpdatedTask = _mapper.Map(updateTaskDto,notUpdatedTask);
        await _context.SaveChangesAsync();
        return _mapper.Map<TaskItemDTO>(UpdatedTask);
    }

    public async Task<IEnumerable<TaskItemDTO>> FilterListTask (TaskFilterDTO taskFilterDto)
    {
        var filteredTasks = await _context.Tasks.ToListAsync();
        filteredTasks = filteredTasks.Where(t => t.Status == taskFilterDto.Status).ToList();
        filteredTasks = filteredTasks.Where(t => t.Priority == taskFilterDto.Priority).ToList();
    
        
        return _mapper.Map<IEnumerable<TaskItemDTO>>(filteredTasks);
    }

    public async Task<TaskItemDTO> GetById (Guid id)
    {
        var taskEntity = await _context.Tasks.FindAsync(id);
        if(taskEntity == null)
        {
            throw new Exception("Task cannot be found.");
        }

        return _mapper.Map<TaskItemDTO>(taskEntity);
    }

    public async  Task<bool> Delete (Guid id)
    {
        var taskEntity = await _context.Tasks.FindAsync(id);
        if (taskEntity == null)
    {
        return false;
    }
        _context.Tasks.Remove(taskEntity);
        await _context.SaveChangesAsync();
            
        return true;
    }

        


}