using Core.Application.Clients.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClient;

public record GetClientQuery(
    Guid Id
    ) : IRequest<ErrorOr<ClientResponse>>;
