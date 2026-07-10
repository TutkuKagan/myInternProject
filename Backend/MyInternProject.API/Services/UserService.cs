using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;


public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UserService(ApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<UserDTO> Register (CreateUserDTO createuserDto)
    {
            _logger.LogInformation("Log of Creating a User. User: {Username}", createuserDto.Username);
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == createuserDto.Username);
            if (existingUser != null)
        {
            throw new Exception("Username already taken");
        }

            var userEntity = _mapper.Map<User>(createuserDto);
            userEntity.PasswordHash = createuserDto.Password;
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();


            _logger.LogInformation("Succesfully created the User. User: {Username}", createuserDto.Username);
            return _mapper.Map<UserDTO>(userEntity);
        
    }

    public async Task<UserDTO> Login (LoginDTO loginDto)
    {
            //note to self: find can only find id's whereas firstorDefault finds the first one, or returns null(default)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password);
            
            if(user == null)
        {
            _logger.LogWarning("Invalid login try. User: {Username}", loginDto.Username);
            throw new Exception("Username or Password is wrong");
        }


             _logger.LogInformation("Succesfully logged in the User. User: {Username}", loginDto.Username);
            return _mapper.Map<UserDTO>(user);
        
    }

    public async  Task<bool> Delete (Guid id)
    {
        _logger.LogInformation("Deleting a User: {Userid}", id);
        var userEntity = await _context.Users.FindAsync(id);
        if (userEntity == null)
    {
        return false;
    }
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
            
        _logger.LogInformation("Deleted the User: {Userid}", id);    
        return true;
    }

    public async Task<UserDTO> GetById (Guid id)
    {
        var userEntity = await _context.Users.FindAsync(id);

        if(userEntity == null)
        {
            throw new Exception("User cannot be found.");
        }

        return _mapper.Map<UserDTO>(userEntity);
    }

    public async Task<UserDTO> UpdateUser (Guid id ,UpdateUserDTO updateUserDto)
    {

        _logger.LogInformation("Updating the User. User: {Username}", updateUserDto.Username);
        var notUpdatedUser = await _context.Users.FindAsync(id);
        if(notUpdatedUser == null)
        {
            throw new Exception("User cannot be found.");
        }

        var UpdatedUser = _mapper.Map(updateUserDto,notUpdatedUser);
        await _context.SaveChangesAsync();


         _logger.LogInformation("Updated the User. User: {Username}", updateUserDto.Username);
        return _mapper.Map<UserDTO>(UpdatedUser);
    }


}