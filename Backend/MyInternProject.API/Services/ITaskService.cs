using MyInternProject.API.DTOs;

namespace MyInternProject.API.Services;



public interface ITaskService
{
    Task<TaskItemDTO> CreateTask(CreateTaskDTO createTaskDto);
    Task<TaskItemDTO> UpdateTask(Guid id  , UpdateTaskDTO updateTaskDto);
    Task<IEnumerable<TaskItemDTO>> FilterListTask(TaskFilterDTO taskFilterDto);
    Task<TaskItemDTO> GetById(Guid id);
    Task<IEnumerable<TaskItemDTO>> GetFilteredTasksAsync(TaskQueryDTO queryDto, Guid userId);
    Task<bool> Delete (Guid id);



    
}