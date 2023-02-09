using Core.Application.Common.Response;
using Core.Application.Users.Commands.AddUser;
using Core.Application.Users.Common;
using Core.Application.Users.Queries.GetUser;
using Core.Application.Users.Queries.GetUsers;
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

        /// <summary>
        /// Get all users
        /// Authenticated user must have ReadUsers permission
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permissions.ReadUsers)]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            GetUsersQuery query = new(pageNumber, pageSize);

            ErrorOr<PaginatedResponse<List<UserResponse>>> result = await _mediator.Send(query, cancellationToken);

            return result.Match(result => Ok(result), error => Problem(error));
        }

        /// <summary>
        /// Get user by id
        /// Authenticated user must have ReadUser permission
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [HasPermission(Permissions.ReadUser)]
        public async Task<IActionResult> GetUserById(string userId, CancellationToken cancellationToken)
        {
            GetUserQuery query = new(userId);

            ErrorOr<UserResponse> result = await _mediator.Send(query, cancellationToken);

            return result.Match(result => Ok(result), error => Problem(error));
        }

        /// <summary>
        /// Add new user
        /// Authenticated user must have AddUser permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
