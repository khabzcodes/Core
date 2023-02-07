﻿using Core.Application.Persistence;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence;

public class UserPermissionsRepository : IUserPermissionsRepository
{
    private readonly ApplicationDbContext _context;
    public UserPermissionsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(UserPermission permission)
    {
        _context.UserPermissions.Add(permission);
        _context.SaveChanges();
    }

    public List<UserPermission> FindAllByUserId(string userId)
    {
        return _context.UserPermissions.Where(x => x.UserId == userId).ToList();
    }

    public async Task<HashSet<string>> GetUserPermissionsAsync(string userId)
    {
        string[] permissions = await _context.UserPermissions
            .Include(x => x.Permission)
            .Where(x => x.UserId == userId).Select(x => x.Permission.Name)
            .ToArrayAsync();

        return permissions.ToHashSet();
    }

    public void Remove(UserPermission permission)
    {
        _context.UserPermissions.Remove(permission);
        _context.SaveChanges();
    }
}
