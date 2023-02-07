using Core.Application.Clients.Commands.CreateClient;
using Core.Application.Clients.Common;
using Core.Application.Clients.Queries.GetClient;
using Core.Application.Clients.Queries.GetClients;
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
        /// Get all clients
        /// Authenticated user must have ReadClients permission
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permissions.ReadClients)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            GetClientsQuery query = new();

            ErrorOr<List<ClientResponse>> results = await _mediator.Send(query, cancellationToken);

            return results.Match(result => Ok(result), error => Problem(error));
        }

        /// <summary>
        /// Get client by id
        /// Authenticated user must have ReadClient permission
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        [HasPermission(Permissions.ReadClient)]
        public async Task<IActionResult> GetById(Guid clientId, CancellationToken cancellationToken)
        {
            GetClientQuery query = new(clientId);

            ErrorOr<ClientResponse> results = await _mediator.Send(query, cancellationToken);

            return results.Match(result => Ok(result), error => Problem(error));
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
