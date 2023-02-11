using Core.Application.Persistence;

namespace Core.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public IClientsRepository ClientsRepository => new ClientsRepository(_context);
    public IUserPermissionsRepository UserPermissionsRepository => new UserPermissionsRepository(_context);
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
