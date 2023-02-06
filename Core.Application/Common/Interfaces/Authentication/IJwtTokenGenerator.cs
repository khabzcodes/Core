using Core.Domain.Entities;

namespace Core.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(ApplicationUser user);
}
