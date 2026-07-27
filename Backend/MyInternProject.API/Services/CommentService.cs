using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CommentService> _logger;

    public CommentService(ApplicationDbContext context, IMapper mapper, ILogger<CommentService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CommentDTO> AddComment(CreateCommentDTO createCommentDto, Guid userId)
    {
    var commentEntity = new TaskComment
    {
        Id = Guid.NewGuid(),
        TaskId = createCommentDto.TaskId,
        Comment = createCommentDto.Comment,
        UserId = userId,
        CreatedAt = DateTime.UtcNow
    };

    _context.TaskComments.Add(commentEntity);
    await _context.SaveChangesAsync();

    await _context.Entry(commentEntity).Reference(c => c.User).LoadAsync();

    return new CommentDTO
    {
        Id = commentEntity.Id,
        TaskId = commentEntity.TaskId,
        UserId = commentEntity.UserId,
        UserName = commentEntity.User?.Username ?? commentEntity.User?.Email ?? "User",
        Comment = commentEntity.Comment,
        CreatedAt = commentEntity.CreatedAt
     };
    }

    public async Task<IEnumerable<CommentDTO>> GetCommentsByTaskId(Guid taskId, CancellationToken cancellationToken = default)
    {
    return await _context.TaskComments
        .Include(c => c.User)
        .Where(c => c.TaskId == taskId)
        .OrderByDescending(c => c.CreatedAt)
        .Select(c => new CommentDTO
        {
            Id = c.Id,
            TaskId = c.TaskId,
            UserId = c.UserId,
            UserName = c.User != null ? c.User.Username : "User",
            Comment = c.Comment,
            CreatedAt = c.CreatedAt
        })
        .ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteComment(Guid commentId, Guid userId)
    {
        _logger.LogWarning("Deleting comment. CommentId: {CommentId}, UserId: {UserId}", commentId, userId);

        var comment = await _context.TaskComments.FindAsync(commentId);
        if (comment == null)
        {
            return false;
        }

        if (comment.UserId != userId)
        {
            _logger.LogWarning("Unauthorized comment deletion attempt. CommentId: {CommentId}, UserId: {UserId}", commentId, userId);
            return false;
        }

        _context.TaskComments.Remove(comment);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Comment deleted successfully. CommentId: {CommentId}", commentId);
        return true;
    }
}