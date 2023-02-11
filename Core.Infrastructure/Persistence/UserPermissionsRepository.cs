using Core.Application.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence;

public class UserPermissionsRepository : GenericRepository<UserPermission>, IUserPermissionsRepository
{
    private readonly ApplicationDbContext _context;
    public UserPermissionsRepository(ApplicationDbContext context)
        :base(context)
    {
        _context = context;
    }

    public List<UserPermission> FindAllByUserId(Guid userId)
    {
        return _context.UserPermissions.Where(x => x.UserId == userId).ToList();
    }

    public async Task<HashSet<string>> GetUserPermissionsAsync(Guid userId)
    {
        string[] permissions = await _context.UserPermissions
            .Include(x => x.Permission)
            .Where(x => x.UserId == userId).Select(x => x.Permission.Name)
            .ToArrayAsync();

        return permissions.ToHashSet();
    }
}
