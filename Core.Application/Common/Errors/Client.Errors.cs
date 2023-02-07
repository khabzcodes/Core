using ErrorOr;

namespace Core.Application.Common.Errors;

public static class ClientErrors
{
    public static Error AlreadyExist(string name) => Error.Conflict(
        code: "Clients.AlreadyExist",
        description: $"{name} client already exist"
        );
}
