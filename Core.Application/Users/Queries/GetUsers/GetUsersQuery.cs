using Core.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<ErrorOr<List<UserResponse>>>;
