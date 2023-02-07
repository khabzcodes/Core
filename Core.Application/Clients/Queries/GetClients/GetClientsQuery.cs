using Core.Application.Clients.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClients;

public record GetClientsQuery() : IRequest<ErrorOr<List<ClientResponse>>>;
