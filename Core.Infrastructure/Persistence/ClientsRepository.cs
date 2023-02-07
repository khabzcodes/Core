﻿using Core.Application.Persistence;
using Core.Domain.Entities;

namespace Core.Infrastructure.Persistence;

public class ClientsRepository : IClientsRepository
{
    private readonly ApplicationDbContext _context;

    public ClientsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
    }

    public List<Client> FindAll()
    {
        return _context.Clients.ToList();
    }

    public Client? FindById(Guid id)
    {
        return _context.Clients.FirstOrDefault(x => x.Id == id);
    }

    public Client? FindByName(string name)
    {
        return _context.Clients.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
    }
}