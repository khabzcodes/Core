using ErrorOr;

namespace Core.Application.Common.Errors;

public static class AuthenticationErrors
{
    public static Error NotFound => Error.NotFound(
        code: "Account.NotFound",
        description: "Email/password incorrect"
        );
}
