using Core.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUser;

public record GetUserQuery(
    Guid UserId
    ) : IRequest<ErrorOr<UserResponse>>;
