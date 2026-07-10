using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyInternProject.API.DTOs;
using MyInternProject.API.Models;

namespace MyInternProject.API.Services;


public class CategoryService : ICategoryService
{
        private readonly ApplicationDbContext _context ;
        private readonly IMapper _mapper ;
        private readonly ILogger _logger;

        public CategoryService(ApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<CategoryDTO> CreateCategory(CreateCategoryDTO createCategoryDto, Guid userid)
    {
             _logger.LogInformation("Creating the Category by user. User: {Userid}", userid);
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.UserId == userid && (c.Name == createCategoryDto.Name || c.Description == createCategoryDto.Description));
            if(existingCategory != null)
                      {
                     throw new Exception("This category is already included.");
                          }    

            var categoryEntity = _mapper.Map<Category>(createCategoryDto);
            categoryEntity.UserId = userid;
             
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created the Category by user. Category: {Name}", createCategoryDto.Name);
            return _mapper.Map<CategoryDTO>(categoryEntity);
    }

    public async Task<CategoryDTO> UpdateCategory (Guid id ,UpdateCategoryDTO updateCategoryDto)
    {
        _logger.LogInformation("Updating the Category. Category: {name}", updateCategoryDto.Name);
        var notUpdatedCategory = await _context.Categories.FindAsync(id);
        if (notUpdatedCategory == null)
        {
            throw new Exception("There is no such Category.");
        }

        var UpdatedCategory = _mapper.Map(updateCategoryDto,notUpdatedCategory);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Updating the Category. Category: {name}", updateCategoryDto.Name);
        return _mapper.Map<CategoryDTO>(UpdatedCategory);

    }

    public async  Task<bool> Delete (Guid id)
    {
        _logger.LogInformation("Deleting the Category. Category: {id}", id);
        var categoryEntity = await _context.Categories.FindAsync(id);
        if (categoryEntity == null)
    {
        return false;
    }
        _context.Categories.Remove(categoryEntity);
        await _context.SaveChangesAsync();
            
        _logger.LogInformation("Deleted the Category. Category: {id}", id);    
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