using MyInternProject.API.DTOs;

namespace MyInternProject.API.Services;

public interface ICommentService
{
    Task<CommentDTO> AddComment(CreateCommentDTO createCommentDto, Guid userId);
    Task<IEnumerable<CommentDTO>> GetCommentsByTaskId(Guid taskId);
}