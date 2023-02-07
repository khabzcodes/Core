using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IPermissionsRepository
{
    Permission? FindByName(string name);
}
