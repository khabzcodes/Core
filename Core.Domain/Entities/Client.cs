namespace Core.Domain.Entities;

public class Client
{
    public Client(Guid id, string? logoUrl, string name, string sector, string emailAddress, DateTime createdAtUtc)
    {
        Id = id;
        LogoUrl = logoUrl;
        Name = name;
        Sector = sector;
        EmailAddress = emailAddress;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; set; }
    public string? LogoUrl { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }

    public static Client Create(Guid id, string? logoUrl, string name, string sector, string emailAddress, DateTime createdAtUtc)
    {
        return new Client(id, logoUrl, name, sector, emailAddress, createdAtUtc);
    }
}


