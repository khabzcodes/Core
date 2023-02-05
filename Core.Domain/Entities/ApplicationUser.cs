using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser(
        string email, 
        string firstName, 
        string lastName, 
        DateTime createdAtUtc)
    {
        Email = email;
        UserName = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedAtUtc = createdAtUtc;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? LastUpdatedAtUtc { get; set; } = null;

    public static ApplicationUser Create(
        string email,
        string firstName,
        string lastName,
        DateTime createdAtUtc
        )
    {
        return new ApplicationUser(email, firstName, lastName, createdAtUtc);
    }
}
