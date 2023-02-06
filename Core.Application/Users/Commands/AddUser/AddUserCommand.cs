using Core.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Commands.AddUser;

public record AddUserCommand(
    string FirstName,
    string LastName,
    string Email,
    HashSet<string> Permissions
    ): IRequest<ErrorOr<UserResponse>>;
