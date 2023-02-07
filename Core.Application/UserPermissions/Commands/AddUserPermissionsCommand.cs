﻿using Core.Application.UserPermissions.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.UserPermissions.Commands;

public record AddUserPermissionsCommand(
    string UserId,
    HashSet<string> Permissions
    ) : IRequest<ErrorOr<List<UserPermissionResponse>>>;
