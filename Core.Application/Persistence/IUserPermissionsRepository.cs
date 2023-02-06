namespace Core.Application.Persistence;

public interface IUserPermissionsRepository
{
    Task<HashSet<string>> GetUserPermissionsAsync(string userId);
}
