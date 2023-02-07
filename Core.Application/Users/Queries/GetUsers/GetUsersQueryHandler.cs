using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Application.Users.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<List<UserResponse>>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserPermissionsRepository _userPermissionsRepository;

    public GetUsersQueryHandler(
        IUsersRepository usersRepository, 
        IUserPermissionsRepository userPermissionsRepository)
    {
        _usersRepository = usersRepository;
        _userPermissionsRepository = userPermissionsRepository;
    }

    public async Task<ErrorOr<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<ApplicationUser> users = _usersRepository.FindAll();

        List<UserResponse> results = new();

        foreach(var user in users)
        {
            List<UserPermission> userPermissionsResponse = _userPermissionsRepository.FindAllByUserId(user.Id);

            UserResponse userResponse = new(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                userPermissionsResponse
                .Select(p => new UserPermissionResponse(
                    p.Id,
                    p.UserId,
                    p.PermissionId))
                .OrderBy(p => p.PermissionId)
                .ToList(),
                user.CreatedAtUtc
                );

            results.Add(userResponse);
        }

        return await Task.FromResult(results);
    }
}
