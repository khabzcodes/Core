using Core.Application.Users.Commands.AddUser;
using Core.Application.Users.Common;
using Core.Contracts.Users;
using Core.Domain.Enums;
using Core.Infrastructure.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UsersController : ApiController
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HasPermission(Permissions.ReadUsers)]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok();
        }

        [HttpPost]
        [HasPermission(Permissions.AddUser)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
        {
            AddUserCommand command = new(request.FirstName, request.LastName, request.Email, request.Permissions);

            ErrorOr<UserResponse> result = await _mediator.Send(command, cancellationToken);

            return result.Match(result => Ok(result), error => Problem(error));
        }
    }
}
