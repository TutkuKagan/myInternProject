using System.Drawing;

namespace MyInternProject.API.Models;


public class Category
{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}


    public string Name {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public string Color {get; set;} = "#007bff";
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    //---------------------------------- NAVIGATION PROP---------------------------------------
    
    public ICollection<TaskItem> TaskItems {get; set;} = new List<TaskItem>();

    public User User {get; set;} = null!;







}