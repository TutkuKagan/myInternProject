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

            CreateMap<TaskItem, TaskItemDTO>()
                .ForMember(dest => dest.AttachmentCount, opt => opt.MapFrom(src => src.TaskAttachments != null ? src.TaskAttachments.Count : 0))
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.TaskComments != null ? src.TaskComments.Count : 0));

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