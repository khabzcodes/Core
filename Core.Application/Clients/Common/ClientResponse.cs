namespace Core.Application.Clients.Common;

public record ClientResponse(
    Guid Id,
    string? LogoUrl,
    string Name,
    string Sector,
    string EmailAddress,
    DateTime CreatedAtUtc
    );
