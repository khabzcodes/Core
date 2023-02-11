using Core.Application.UserPermissions.Common;

namespace Core.Application.Users.Common;

public record UserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    List<UserPermissionResponse> Permissions,
    DateTime CreatedAtUtc
    );


