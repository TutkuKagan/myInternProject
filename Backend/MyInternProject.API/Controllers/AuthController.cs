namespace MyInternProject.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyInternProject.API.DTOs;
using MyInternProject.API.Services;
using MyInternProject.API.Services;

[ApiController] 
[Route("api/[controller]")]
public class AuthController : ControllerBase 
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public AuthController(IJwtService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login (LoginDTO loginDto)
    {
            var user = await _userService.Login(loginDto);

            var token = _jwtService.GenerateToken(user.Id, user.Username);

            return Ok(new 
        { 
            User = user, 
            Token = token 
        });

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDTO createUserDto)
    {

        var user = await _userService.Register(createUserDto);
        return Ok(user);
    }


    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

    if (string.IsNullOrEmpty(userIdClaim))
    {
        return Unauthorized("User not found");
    }

    var userId = Guid.Parse(userIdClaim);
    var userProfile = await _userService.GetById(userId); 

    
    return Ok(userProfile);
    }

}