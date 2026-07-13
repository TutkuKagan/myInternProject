using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;



public interface ITaskService
{
    Task<TaskItemDTO> CreateTask(CreateTaskDTO createTaskDto,Guid id);
    Task<TaskItemDTO> UpdateTask(Guid id  , UpdateTaskDTO updateTaskDto);
    Task<IEnumerable<TaskItemDTO>> FilterListTask(TaskFilterDTO taskFilterDto);
    Task<TaskItemDTO> GetById(Guid id);
    Task<IEnumerable<TaskItemDTO>> GetFilteredTasksAsync(TaskQueryDTO queryDto, Guid userId,CancellationToken cancellationToken);
    Task<bool> Delete (Guid id);

    Task<TaskAttachment> UploadAttachmentAsync(UploadAttachmentDTO uploadDto);

    Task<IEnumerable<TaskItemDTO>> GetOverdueTasks(Guid userId);
    Task<TaskStatisticsDTO> GetTaskStatistics(Guid userId);



    
}