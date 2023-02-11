namespace Core.Application.UserPermissions.Common;

public record UserPermissionResponse(
    Guid Id,
    Guid UserId,
    int PermissionId
    );
