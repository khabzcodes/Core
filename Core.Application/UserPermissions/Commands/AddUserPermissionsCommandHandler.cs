using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.UserPermissions.Commands;

public class AddUserPermissionsCommandHandler :
    IRequestHandler<AddUserPermissionsCommand, ErrorOr<List<UserPermissionResponse>>>
{
    private readonly IUserPermissionsRepository _userPermissionsRepository;
    private readonly IPermissionsRepository _permissionsRepository;

    public AddUserPermissionsCommandHandler(
        IUserPermissionsRepository userPermissionsRepository, 
        IPermissionsRepository permissionsRepository)
    {
        _userPermissionsRepository = userPermissionsRepository;
        _permissionsRepository = permissionsRepository;
    }

    public async Task<ErrorOr<List<UserPermissionResponse>>> Handle(AddUserPermissionsCommand request, CancellationToken cancellationToken)
    {
        List<UserPermission> userPermissions = new();

        foreach(var permission in request.Permissions)
        {
            Permission? permissionExist = _permissionsRepository.FindByName(permission.ToUpper());
            if (permissionExist is null) return PermissionErrors.NotFound(permission.ToUpper());

            UserPermission userPermission = UserPermission.Create(
                Guid.NewGuid(), 
                request.UserId, 
                permissionExist.Id);

            userPermissions.Add(userPermission);
        }

        foreach(var userPermission in userPermissions)
        {
            if (!_userPermissionsRepository
                .FindAllByUserId(request.UserId)
                .Any(p => p.PermissionId == userPermission.PermissionId))
            {
                _userPermissionsRepository.Add(userPermission);
            }
        }

        List<UserPermission> currentUserPermissions = _userPermissionsRepository.FindAllByUserId(request.UserId);

        List<UserPermissionResponse> result = currentUserPermissions
            .Select(p => new UserPermissionResponse(
                p.Id, 
                p.UserId, 
                p.PermissionId))
            .OrderBy(p => p.PermissionId)
            .ToList();

        return await Task.FromResult(result);
    }
}