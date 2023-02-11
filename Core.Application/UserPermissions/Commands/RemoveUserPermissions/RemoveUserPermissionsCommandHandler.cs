using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.UserPermissions.Commands.RemoveUserPermissions;

public class RemoveUserPermissionsCommandHandler :
    IRequestHandler<RemoveUserPermissionsCommand, ErrorOr<List<UserPermissionResponse>>>
{
    private readonly IUserPermissionsRepository _userPermissionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserPermissionsCommandHandler(
        IUserPermissionsRepository userPermissionsRepository, 
        IUnitOfWork unitOfWork)
    {
        _userPermissionsRepository = userPermissionsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<UserPermissionResponse>>> Handle(
        RemoveUserPermissionsCommand request, 
        CancellationToken cancellationToken)
    {
        List<UserPermission> permissions = _userPermissionsRepository.FindAllByUserId(request.UserId);

        foreach(var permission in request.Permissions)
        {
            UserPermission? permissionToDelete = permissions.Find(x => x.Id == permission);

            if (permissionToDelete is not null)
            {
                _userPermissionsRepository.Remove(permissionToDelete);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        List<UserPermission> availablePermissions = _userPermissionsRepository.FindAllByUserId(request.UserId);

        List<UserPermissionResponse> result = availablePermissions
            .Select(p => new UserPermissionResponse(
                p.Id, 
                p.UserId, 
                p.PermissionId))
            .OrderBy(x => x.PermissionId)
            .ToList();
        
        return result;
    }
}

