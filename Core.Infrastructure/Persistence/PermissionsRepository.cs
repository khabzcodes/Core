using Core.Application.Persistence;
using Core.Domain.Entities;

namespace Core.Infrastructure.Persistence;
public class PermissionsRepository : IPermissionsRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Permission? FindByName(string name)
    {
        return _context.Permissions.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
    }
}
