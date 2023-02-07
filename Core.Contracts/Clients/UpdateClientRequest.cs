namespace Core.Contracts.Clients;

public record UpdateClientRequest(
    string Name,
    string Sector,
    string EmailAddress
    );
