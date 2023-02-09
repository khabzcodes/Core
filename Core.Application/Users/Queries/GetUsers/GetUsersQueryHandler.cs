using Core.Application.Common.Response;
using Core.Application.Persistence;
using Core.Application.UserPermissions.Common;
using Core.Application.Users.Common;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<PaginatedResponse<List<UserResponse>>>>
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

    public async Task<ErrorOr<PaginatedResponse<List<UserResponse>>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        List<ApplicationUser> users = _usersRepository.FindAll();

        List<UserResponse> userResponse = users
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

        int totalPages = (int)Math.Ceiling(users.Count / (double)query.PageSize);

        PaginatedResponse<List<UserResponse>> results = new(
            userResponse, 
            query.PageNumber, 
            totalPages, 
            users.Count, 
            query.PageNumber > 1, 
            query.PageNumber < totalPages);

        return await Task.FromResult(results);
    }
}
