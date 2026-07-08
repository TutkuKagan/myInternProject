    namespace MyInternProject.API.Models;

    public class User 
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


        //---------------------------------- NAVIGATION PROP------------------------------------------
        
        
        //--------------------(any user can have more than 1 task ) --------------
        public ICollection<TaskItem> TaskItems {get; set;} = new List<TaskItem>();
        
        //-------------- (any user can have more than 1 category) ---------------------
        public ICollection<Category> Categories {get; set;} = new List<Category>();
        
        //more than one comment
        public ICollection<TaskComment> TaskComments {get; set;} = new List<TaskComment>();

        



    }