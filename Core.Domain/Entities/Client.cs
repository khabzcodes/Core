using Core.Domain.Primitives;

namespace Core.Domain.Entities;

public sealed class Client : Entity
{
    private Client(
        Guid id, 
        string? logoUrl, 
        string name, 
        string sector, 
        string emailAddress, 
        DateTime createdAtUtc)
        : base(id)
    {
        LogoUrl = logoUrl;
        Name = name;
        Sector = sector;
        EmailAddress = emailAddress;
        CreatedAtUtc = createdAtUtc;
    }

    public string? LogoUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }

    public static Client Create(
        Guid id, 
        string? logoUrl, 
        string name, 
        string sector, 
        string emailAddress, 
        DateTime createdAtUtc)
    {
        return new Client(id, logoUrl, name, sector, emailAddress, createdAtUtc);
    }
}


