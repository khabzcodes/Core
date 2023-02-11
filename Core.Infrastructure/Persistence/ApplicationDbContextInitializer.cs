using Core.Domain.Constants;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    public ApplicationDbContextInitializer(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger<ApplicationDbContextInitializer> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating data");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedPermissionsAsync();
            await SeedAdminMemberAysnc();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while seeding data to database");
            throw;
        }
    }

    public async Task SeedAdminMemberAysnc()
    {
        try
        {
            if (await _userManager.FindByEmailAsync(Constants.AdminEmail) == null)
            {
                ApplicationUser user = new(
                    Guid.NewGuid(),
                    Constants.AdminEmail,
                    Constants.AdminFirstName,
                    Constants.AdminLastName,
                    DateTime.UtcNow
                );

                IdentityResult identityResult = await _userManager.CreateAsync(user, Constants.AdminPassword);

                // Add all permissions to admin user
                if (identityResult.Succeeded)
                {
                    List<Permission> permissions = _context.Permissions.ToList();

                    foreach (Permission permission in permissions)
                    {
                        UserPermission userPermission = new(Guid.NewGuid(), user.Id, permission.Id);
                        await _context.UserPermissions.AddAsync(userPermission);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while trying to seed admin user to database");
        }
    }

    public async Task SeedPermissionsAsync()
    {
        try
        {
            IEnumerable<Permission> permissions = Enum
                .GetValues<Permissions>()
                .Select(p => new Permission
                {
                    Id = (int)p,
                    Name = p.ToString(),
                });

            foreach (Permission permission in permissions)
            {
                if (_context.Permissions.All(x => x.Name != permission.Name))
                {
                    await _context.Permissions.AddAsync(permission);
                    await _context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while trying to seed permissions to database");
            throw;
        }
    }
}
