using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Application.Users.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<UserResponse>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserPermissionsRepository _userPermissionsRepository;

    public GetUserQueryHandler(
        IUsersRepository usersRepository, 
        IUserPermissionsRepository userPermissionsRepository)
    {
        _usersRepository = usersRepository;
        _userPermissionsRepository = userPermissionsRepository;
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _usersRepository.GetByIdAsync(query.UserId);
        if (user == null) return UserErrors.NotFound(query.UserId);

        List<UserPermission> permissions = _userPermissionsRepository.FindAllByUserId(query.UserId);

        UserResponse result = new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            permissions
            .Select(p => new UserPermissionResponse(
                p.Id,
                p.UserId,
                p.PermissionId))
            .OrderBy(x => x.PermissionId)
            .ToList(),
            user.CreatedAtUtc
            );

        return await Task.FromResult(result);
    }
}
