using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IClientsRepository : IGenericRepository<Client>
{
    Client? FindByName(string name);
}
