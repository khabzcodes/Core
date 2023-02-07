using Core.Domain.Entities;

namespace Core.Application.Persistence;

public interface IUsersRepository
{
    List<ApplicationUser> FindAll();
    ApplicationUser? FindById(string id);
}
