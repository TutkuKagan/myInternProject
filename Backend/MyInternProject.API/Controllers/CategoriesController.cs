namespace myInternProject.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myInternProject.API.DTOs;
using MyInternProject.API.Services;



[Authorize]
[ApiController] 
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
        private readonly ICategoryService _categoryService;


        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDto)
    {
        var newCategory = await _categoryService.CreateCategory(createCategoryDto);

        return Ok(newCategory);

    }

    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDTO updateCategoryDto)
    {
        var updatedCategory = await _categoryService.UpdateCategory(id, updateCategoryDto);

        return Ok(updatedCategory);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.Delete(id);

        return Ok("Category deleted successfully.");
    }

    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetById(id);

        return Ok(category);
    }




}