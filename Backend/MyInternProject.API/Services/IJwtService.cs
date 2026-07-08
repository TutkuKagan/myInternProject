namespace MyInternProject.API.Services;

public interface IJwtService
{
    string GenerateToken(Guid id, string username);
}