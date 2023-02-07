using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IUserPermissionsRepository
{
    Task<HashSet<string>> GetUserPermissionsAsync(string userId);
    void Add(UserPermission permission);
    List<UserPermission> FindAllByUserId(string userId);
    void Remove(UserPermission permission);
}
