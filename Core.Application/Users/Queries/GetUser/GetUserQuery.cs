using Core.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUser;

public record GetUserQuery(
    string UserId
    ) : IRequest<ErrorOr<UserResponse>>;
