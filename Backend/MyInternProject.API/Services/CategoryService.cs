using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;


public class CategoryService : ICategoryService
{
        private readonly ApplicationDbContext _context ;
        private readonly IMapper _mapper ;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<CategoryDTO> CreateCategory(CreateCategoryDTO createCategoryDto, Guid userid)
    {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.UserId == userid && (c.Name == createCategoryDto.Name || c.Description == createCategoryDto.Description));
            if(existingCategory != null)
                      {
                     throw new Exception("This category is already included.");
                          }    

            var categoryEntity = _mapper.Map<Category>(createCategoryDto);
            categoryEntity.UserId = userid;
             
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<CategoryDTO> UpdateCategory (Guid id ,UpdateCategoryDTO updateCategoryDto)
    {
        var notUpdatedCategory = await _context.Categories.FindAsync(id);
        if (notUpdatedCategory == null)
        {
            throw new Exception("There is no such Category.");
        }

        var UpdatedCategory = _mapper.Map(updateCategoryDto,notUpdatedCategory);
        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDTO>(UpdatedCategory);

    }

    public async  Task<bool> Delete (Guid id)
    {
        var categoryEntity = await _context.Categories.FindAsync(id);
        if (categoryEntity == null)
    {
        return false;
    }
        _context.Categories.Remove(categoryEntity);
        await _context.SaveChangesAsync();
            
        return true;
    }

    public async Task<CategoryDTO> GetById (Guid id)
    {
        var categoryEntity = await _context.Categories.FindAsync(id);
        if(categoryEntity == null)
        {
            throw new Exception("Category cannot be found.");
        }

        return _mapper.Map<CategoryDTO>(categoryEntity);
    }


}