using Core.Application.UserPermissions.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.UserPermissions.Commands.RemoveUserPermissions;

public record RemoveUserPermissionsCommand(
    Guid UserId,
    HashSet<Guid> Permissions
    ) : IRequest<ErrorOr<List<UserPermissionResponse>>>;
