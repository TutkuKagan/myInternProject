using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CommentService(ApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CommentDTO> AddComment(CreateCommentDTO createCommentDto, Guid userId)
    {
        _logger.LogInformation("Adding a Comment. User: {id}", userId);
        var taskExists = await _context.Tasks.AnyAsync(t => t.Id == createCommentDto.TaskId);
        if (!taskExists)
        {
            throw new Exception("Task not found.");
        }

        var commentEntity = _mapper.Map<TaskComment>(createCommentDto);
        
        commentEntity.UserId = userId;
        commentEntity.CreatedAt = DateTime.UtcNow;

        _context.TaskComments.Add(commentEntity);
        await _context.SaveChangesAsync();


        _logger.LogInformation("Added the Comment. Comment's TaskID: {Taskid}", createCommentDto.TaskId);
        return _mapper.Map<CommentDTO>(commentEntity);
    }

    public async Task<IEnumerable<CommentDTO>> GetCommentsByTaskId(Guid taskId)
    {

        var comments = await _context.TaskComments
            .Where(c => c.TaskId == taskId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CommentDTO>>(comments);
    }
}