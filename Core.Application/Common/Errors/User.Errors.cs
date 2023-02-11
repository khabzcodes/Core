using ErrorOr;

namespace Core.Application.Common.Errors;

public static class UserErrors
{
    public static Error AlreadyExist(string email) => Error.Conflict(
        code: "Users.AlreadyExist",
        description: $"User with {email} email already exist"
        );

    public static Error NotFound(Guid userId) => Error.NotFound(
        code: "Users.NotFound",
        description: $"User with user id {userId} not found"
        );
}
