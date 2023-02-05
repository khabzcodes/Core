using Core.Application.Authentication.Common;
using Core.Application.Common.Errors;
using Core.Application.Common.Interfaces.Authentication;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(
        UserManager<ApplicationUser> userManager, 
        IJwtTokenGenerator jwtTokenGenerator
        )
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(query.Email);
        if (user == null) return AuthenticationErrors.NotFound;

        bool checkPassword = await _userManager.CheckPasswordAsync(user, query.Password);
        if (!checkPassword) return AuthenticationErrors.NotFound;

        string? token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Email);
        if (token == null) return AuthenticationErrors.NotFound;

        return new AuthenticationResponse(token);
    }
}
