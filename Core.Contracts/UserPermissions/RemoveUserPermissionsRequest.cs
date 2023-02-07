namespace Core.Contracts.UserPermissions;

public record RemoveUserPermissionsRequest(
    HashSet<Guid> Permissions
    );
