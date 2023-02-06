using Core.Domain.Enums;
using Core.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UsersController : ApiController
    {
        [HasPermission(Permissions.ReadUsers)]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok();
        }
    }
}
