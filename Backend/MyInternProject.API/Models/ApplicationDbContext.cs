using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace MyInternProject.API.Models;



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


    //-------------------------------------fluentAPI--------------------------------------

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.HasIndex(u => u.Username).IsUnique();

            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);

            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);

            entity.Property(u => u.LastName).IsRequired().HasMaxLength(50);

            entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(u => u.IsActive).HasDefaultValue(true);


            //Demo User

            entity.HasData(new User { 
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Email = "deneme@gmail.com" ,
                Username = "DemoPlayer1",
                PasswordHash = "Deneme",
                FirstName = "Tutku",
                LastName = "Bayir",
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                    });


        });

        modelBuilder.Entity<TaskItem>(entity =>
        {
           entity.HasKey(t => t.Id);

            entity.Property(t => t.Title).IsRequired().HasMaxLength(200);

            entity.Property(t => t.Description);

            entity.Property(t => t.Priority).IsRequired();
            entity.Property(t => t.Status).IsRequired();

            entity.Property(t => t.DueDate);
            entity.Property(t => t.CompletedAt);

            entity.Property(t => t.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(t => t.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



            entity.HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Category)
                .WithMany(c => c.TaskItems)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

        });


        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Description);
            entity.Property(c => c.Color).IsRequired().HasMaxLength(7).HasDefaultValue("#007bff");
            entity.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


            entity.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);



        });



       modelBuilder.Entity<TaskAttachment>(entity =>
       {
            entity.HasKey(ta => ta.Id);

            entity.Property(ta => ta.FileName).IsRequired().HasMaxLength(255);
            entity.Property(ta => ta.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(ta => ta.FileSize).IsRequired();
            entity.Property(ta => ta.ContentType).IsRequired().HasMaxLength(100);
            entity.Property(ta => ta.UploadedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


            entity.HasOne(ta => ta.TaskItem).WithMany(t => t.TaskAttachments)
            .HasForeignKey(ta => ta.TaskId)
            .OnDelete(DeleteBehavior.Cascade);


       });


        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.HasKey(tc => tc.Id);

            entity.Property(ta => ta.Comment).IsRequired();
            entity.Property(ta => ta.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


            entity.HasOne(tc => tc.TaskItem)
            .WithMany(t => t.TaskComments)
            .HasForeignKey(tc => tc.TaskId)
            .OnDelete(DeleteBehavior.Cascade);


            entity.HasOne(tc => tc.User)
            .WithMany(u => u.TaskComments)
            .HasForeignKey(tc => tc.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        });

        


    }



}