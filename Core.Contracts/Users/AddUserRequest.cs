namespace Core.Contracts.Users;

public record AddUserRequest(
    string FirstName,
    string LastName,
    string Email,
    HashSet<string> Permissions
    );
