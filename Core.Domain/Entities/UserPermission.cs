using Core.Domain.Primitives;

namespace Core.Domain.Entities;

public class UserPermission : Entity
{
    public UserPermission(Guid id, Guid userId, int permissionId)
        : base(id)
    {
        UserId = userId;
        PermissionId = permissionId;
    }

    public Guid UserId { get; set; }
    public int PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public static UserPermission Create(
        Guid id, 
        Guid userId, 
        int permissionId)
    {
        return new UserPermission(id, userId, permissionId);
    }
}
