using System.Numerics;

namespace myInternProject.API.Models;

public class TaskAttachment
{
    public Guid Id {get; set;}
    public Guid TaskId {get; set;}


    public string FileName {get; set;} = string.Empty;
    public string FilePath {get; set;} = string.Empty;
    public BigInteger FileSize {get; set;} 
    public string ContentType {get; set;} = string.Empty;
    public DateTime UploadedAt {get; set;} = DateTime.UtcNow;



     //---------------------------------- NAVIGATION PROP-------------------------------------

     public TaskItem TaskItem {get; set;} = null!;

}