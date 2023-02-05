namespace Core.Domain.Entities;

public class UserPermission
{
    public UserPermission(Guid id, string userId, int permissionId)
    {
        Id = id;
        UserId = userId;
        PermissionId = permissionId;
    }

    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = null!;

    public static UserPermission Create(Guid id, string userId, int permissionId)
    {
        return new UserPermission(id, userId, permissionId);
    }
}
