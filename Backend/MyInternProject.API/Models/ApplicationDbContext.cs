using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace myInternProject.API.Models;



public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {    
    } 

        public DbSet<TaskItem> Tasks {get; set;}
        public DbSet<TaskComment> TaskComments {get; set;}
        public DbSet<TaskAttachment> TaskAttachments {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<User> Users {get; set;}





}