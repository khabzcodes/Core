using Core.Application.Clients.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Commands.UpdateClient;

public record UpdateClientCommand(
    Guid Id,
    string Name,
    string Sector,
    string EmailAddress
    ) : IRequest<ErrorOr<ClientResponse>>;
