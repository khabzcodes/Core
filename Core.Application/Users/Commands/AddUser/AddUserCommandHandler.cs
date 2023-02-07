using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Application.Users.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Users.Commands.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<UserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPermissionsRepository _permissionsRepository;
    private readonly IUserPermissionsRepository _userPermissionsRepository;

    public AddUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IPermissionsRepository permissionsRepository,
        IUserPermissionsRepository userPermissionsRepository)
    {
        _userManager = userManager;
        _permissionsRepository = permissionsRepository;
        _userPermissionsRepository = userPermissionsRepository;
    }

    public async Task<ErrorOr<UserResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if ((await _userManager.FindByEmailAsync(request.Email)) != null) return UserErrors.AlreadyExist(request.Email);

        ApplicationUser user = ApplicationUser.Create(request.Email, request.FirstName, request.LastName, DateTime.UtcNow);

        IdentityResult identityResult = await _userManager.CreateAsync(user, "Testing@123");

        // Add all permissions to admin user
        if (!identityResult.Succeeded)
        {
            return Error.Failure();
        }

        List<UserPermission> userPermissions = new();

        foreach(var permission in request.Permissions)
        {
            Permission? findPermission = _permissionsRepository.FindByName(permission.ToUpper());
            if (findPermission != null)
            {
                UserPermission userPermission = UserPermission.Create(
                    Guid.NewGuid(),
                    user.Id,
                    findPermission.Id);

                _userPermissionsRepository.Add(userPermission);
                userPermissions.Add(userPermission);
            }
        }

        List<UserPermissionResponse> userPermissionResponse = userPermissions
            .Select(up => new UserPermissionResponse(
                up.Id, 
                up.UserId, 
                up.PermissionId))
            .ToList();

        UserResponse result = new(
            user.Id, 
            user.FirstName, 
            user.LastName, 
            user.Email, 
            userPermissionResponse, 
            user.CreatedAtUtc);

        return result;
    }
}
