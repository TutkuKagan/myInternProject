using MyInternProject.API.DTOs;
namespace MyInternProject.API.Services;



public interface ICategoryService
{
    Task<CategoryDTO> CreateCategory(CreateCategoryDTO createCategoryDto);
    Task<CategoryDTO> UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDto);
    Task<bool> Delete (Guid id);
    Task<CategoryDTO> GetById(Guid id);
    
    
}