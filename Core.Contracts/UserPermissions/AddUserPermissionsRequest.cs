namespace Core.Contracts.UserPermissions;

public record AddUserPermissionsRequest(
    HashSet<string> Permissions
    );
