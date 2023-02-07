using Core.Application.Clients.Common;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Commands.CreateClient;

public record CreateClientCommand(
    string Name,
    string Sector,
    string EmailAddress
    ) : IRequest<ErrorOr<ClientResponse>>;
