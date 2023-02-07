﻿using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IClientsRepository
{
    void Add(Client client);
    Client? FindByName(string name);
    List<Client> FindAll();
    Client? FindById(Guid id);
    Client Update(Client client);
}
