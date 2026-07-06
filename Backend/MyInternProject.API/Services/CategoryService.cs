using AutoMapper;
using myInternProject.API.DTOs;
using myInternProject.API.Models;

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


    public async Task<CategoryDTO> CreateCategory (CreateCategoryDTO createCategoryDto)
    {
            var categoryEntity = _mapper.Map<Category>(createCategoryDto);
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(categoryEntity);
        
    }

    public async Task<CategoryDTO> UpdateCategory (Guid id ,UpdateCategoryDTO updateCategoryDto)
    {
        var notUpdatedCategory = await _context.Categories.FindAsync(id);

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

        return _mapper.Map<CategoryDTO>(categoryEntity);
    }


}