using Core.Application.Persistence;
using Core.Domain.Entities;

namespace Core.Infrastructure.Persistence;

public class ClientsRepository : GenericRepository<Client>, IClientsRepository
{
    private readonly ApplicationDbContext _context;

    public ClientsRepository(ApplicationDbContext context)
        :base(context)
    {
        _context = context;
    }

    public Client? FindByName(string name)
    {
        return _context.Clients.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
    }
}
