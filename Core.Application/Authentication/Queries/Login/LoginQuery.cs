using Core.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResponse>>;
