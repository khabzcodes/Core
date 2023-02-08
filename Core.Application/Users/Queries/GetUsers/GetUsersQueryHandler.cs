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

    public async Task<ErrorOr<List<UserResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        List<UserResponse> users = _usersRepository
            .FindAll()
            .OrderBy(x => x.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(u => new UserResponse(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                _userPermissionsRepository.FindAllByUserId(u.Id)
                .Select(p => new UserPermissionResponse(
                    p.Id, 
                    p.UserId, 
                    p.PermissionId))
                .OrderBy(x => x.PermissionId)
                .ToList(),
                u.CreatedAtUtc
                ))
            .ToList();

        return await Task.FromResult(users.ToList());
    }
}
