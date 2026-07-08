namespace MyInternProject.API.DTOs;
using System.ComponentModel.DataAnnotations;


public class UpdateUserDTO
{
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required,EmailAddress,MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required,MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;



}

