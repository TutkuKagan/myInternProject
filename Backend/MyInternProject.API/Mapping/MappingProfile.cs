using AutoMapper;
using myInternProject.API.DTOs;
using myInternProject.API.Models;

namespace myInternProject.API.Mapping;


public class MappingProfile:Profile
{
        public MappingProfile()
        {
            //outputs
            CreateMap<User, UserDTO>();
            CreateMap<TaskItem, TaskItemDTO>();
            CreateMap<Category,CategoryDTO>();

            //inputs
            CreateMap<CreateUserDTO,User>();
            CreateMap<UpdateUserDTO,User>();
            CreateMap<LoginDTO,User>();

            CreateMap<CreateCategoryDTO,Category>();
            CreateMap<UpdateCategoryDTO,Category>();
            
            CreateMap<UpdateTaskDTO, TaskItem>();
            CreateMap<CreateTaskDTO, TaskItem>();
            CreateMap<TaskFilterDTO, TaskItem>();
        }

}