using Core.Application.UserPermissions.Commands.AddUserPermissions;
using Core.Application.UserPermissions.Commands.RemoveUserPermissions;
using Core.Application.UserPermissions.Common;
using Core.Contracts.UserPermissions;
using Core.Domain.Enums;
using Core.Infrastructure.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Authorize]
    [Route("api/userPermissions")]
    public class UserPermissionsController : ApiController
    {
        private readonly ISender _mediator;

        public UserPermissionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{userId}")]
        [HasPermission(Permissions.UpdateUser)]
        public async Task<IActionResult> AddPermissions(
            Guid userId, 
            [FromBody] AddUserPermissionsRequest request, 
            CancellationToken cancellationToken)
        {
            AddUserPermissionsCommand command = new(userId, request.Permissions);

            ErrorOr<List<UserPermissionResponse>> result = await _mediator.Send(command, cancellationToken);

            return result.Match(result => Ok(result), error => Problem(error));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Remove(
            Guid userId, 
            [FromBody] RemoveUserPermissionsRequest request, 
            CancellationToken cancellationToken)
        {
            RemoveUserPermissionsCommand command = new(userId, request.Permissions);

            ErrorOr<List<UserPermissionResponse>> result = await _mediator.Send(command, cancellationToken);

            return result.Match(result => Accepted(result), error => Problem(error));
        }
    }
}
