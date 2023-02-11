using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser(
        Guid id,
        string email, 
        string firstName, 
        string lastName, 
        DateTime createdAtUtc)
    {
        Id = id;
        Email = email;
        UserName = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedAtUtc = createdAtUtc;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IReadOnlyCollection<UserPermission> UserPermissions { get; set; } = null!;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? LastUpdatedAtUtc { get; set; } = null;

    public static ApplicationUser Create(
        Guid id,
        string email,
        string firstName,
        string lastName,
        DateTime createdAtUtc
        )
    {
        return new ApplicationUser(id, email, firstName, lastName, createdAtUtc);
    }
}
