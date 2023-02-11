using Core.Application.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence;

public class UsersRepository : GenericRepository<ApplicationUser>, IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
        :base(context)
    {
        _context = context;
    }
}
