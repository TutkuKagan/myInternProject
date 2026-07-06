using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myInternProject.API.DTOs;
using myInternProject.API.Models;

namespace MyInternProject.API.Services;


public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<UserDTO> Register (CreateUserDTO createuserDto)
    {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == createuserDto.Username);
            if (existingUser != null)
        {
            throw new Exception("Username already taken");
        }

            var userEntity = _mapper.Map<User>(createuserDto);
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(userEntity);
        
    }

    public async Task<UserDTO> Login (LoginDTO loginDto)
    {
            //note to self: find can only find id's whereas firstorDefault finds the first one, or returns null(default)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.Password);
            
            if(user == null)
        {
            throw new Exception("Username or Password is wrong");
        }

            return _mapper.Map<UserDTO>(user);
        
    }

    public async  Task<bool> Delete (Guid id)
    {
        var userEntity = await _context.Users.FindAsync(id);
        if (userEntity == null)
    {
        return false;
    }
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
            
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
        var notUpdatedUser = await _context.Users.FindAsync(id);
        if(notUpdatedUser == null)
        {
            throw new Exception("User cannot be found.");
        }

        var UpdatedUser = _mapper.Map(updateUserDto,notUpdatedUser);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(UpdatedUser);
    }


}