using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IUserPermissionsRepository : IGenericRepository<UserPermission>
{
    Task<HashSet<string>> GetUserPermissionsAsync(Guid userId);
    List<UserPermission> FindAllByUserId(Guid userId);
}
