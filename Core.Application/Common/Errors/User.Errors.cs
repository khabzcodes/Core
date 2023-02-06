using ErrorOr;

namespace Core.Application.Common.Errors;

public static class UserErrors
{
    public static Error AlreadyExist(string email) => Error.Conflict(
        code: "Users.AlreadyExist",
        description: $"User with {email} email already exist"
        );
}
