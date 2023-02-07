namespace Core.Application.UserPermissions.Common;

public record UserPermissionResponse(
    Guid Id,
    string UserId,
    int PermissionId
    );
