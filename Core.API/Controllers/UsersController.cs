using Core.Domain.Enums;
using Core.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiController
    {
        [HasPermission(Permissions.ReadUser)]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok();
        }
    }
}
