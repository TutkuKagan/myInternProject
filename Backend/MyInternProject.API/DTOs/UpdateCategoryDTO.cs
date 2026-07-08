namespace MyInternProject.API.DTOs;
using System.ComponentModel.DataAnnotations;

public class UpdateCategoryDTO 
{
    [Required, MaxLength(100)]
    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    [Required, MaxLength(7)]
    public string Color {get; set;} = "#007bff";

}