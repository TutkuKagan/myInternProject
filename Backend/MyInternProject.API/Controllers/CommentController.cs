using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInternProject.API.DTOs;
using MyInternProject.API.Services;
using System.Security.Claims;

namespace MyInternProject.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddComment(CreateCommentDTO createCommentDto)
    {

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("User identity not found.");
        }

        var userId = Guid.Parse(userIdClaim);
        
        var result = await _commentService.AddComment(createCommentDto, userId);
        return Ok(result);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetComments(Guid taskId)
    {
        var comments = await _commentService.GetCommentsByTaskId(taskId);
        return Ok(comments);
    }
}