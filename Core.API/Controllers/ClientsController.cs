using Core.Application.Clients.Commands.CreateClient;
using Core.Application.Clients.Common;
using Core.Contracts.Clients;
using Core.Domain.Enums;
using Core.Infrastructure.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers
{
    [Authorize]
    [Route("api/clients")]
    public class ClientsController : ApiController
    {
        private readonly ISender _mediator;
        public ClientsController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new client
        /// Authenticated user must have AddClient permission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(Permissions.AddClient)]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest request, CancellationToken cancellationToken)
        {
            CreateClientCommand command = new(request.Name, request.Sector, request.EmailAddress);

            ErrorOr<ClientResponse> result = await _mediator.Send(command, cancellationToken);

            return result.Match(result => Ok(result), error => Problem(error));
        }
    }
}
