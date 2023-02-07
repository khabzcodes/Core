using ErrorOr;

namespace Core.Application.Common.Errors;

public static class ClientErrors
{
    public static Error AlreadyExist(string name) => Error.Conflict(
        code: "Clients.AlreadyExist",
        description: $"{name} client already exist"
        );

    public static Error NotFound => Error.NotFound(
        code: "Clients.NotFound",
        description: "Client not found"
        );
}
