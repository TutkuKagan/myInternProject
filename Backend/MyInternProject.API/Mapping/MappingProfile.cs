using AutoMapper;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Mapping;


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


            CreateMap<CreateCommentDTO, TaskComment>();
            CreateMap<TaskComment, CommentDTO>();
        }

}