using Core.Application.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ApplicationUser> FindAll()
    {
        return _context.Users.AsNoTracking().ToList();
    }

    public ApplicationUser? FindById(string id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
