using MyInternProject.API.DTOs;

namespace MyInternProject.API.Services;



public interface IUserService
{
        Task<UserDTO> Register (CreateUserDTO CreateUserDto);
        Task<UserDTO> Login (LoginDTO LoginDto);
        Task<UserDTO> GetById (Guid id);
        Task<UserDTO> UpdateUser (Guid id, UpdateUserDTO updateUserDto);
        Task<bool> Delete (Guid id);


    
}