namespace Core.Contracts.Clients;

public record CreateClientRequest(
    string Name,
    string Sector,
    string EmailAddress
    );
