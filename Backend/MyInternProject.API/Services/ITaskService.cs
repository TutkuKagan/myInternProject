using myInternProject.API.DTOs;

namespace MyInternProject.API.Services;



public interface ITaskService
{
    Task<TaskItemDTO> CreateTask(CreateTaskDTO createTaskDto);
    Task<TaskItemDTO> UpdateTask(Guid id  , UpdateTaskDTO updateTaskDto);
    Task<IEnumerable<TaskItemDTO>> FilterListTask(TaskFilterDTO taskFilterDto);
    Task<TaskItemDTO> GetById(Guid id);
    Task<bool> Delete (Guid id);



    
}