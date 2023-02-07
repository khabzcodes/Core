using Core.Application.Clients.Common;
using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ErrorOr<ClientResponse>>
{
    private readonly IClientsRepository _clientsRepository;

    public CreateClientCommandHandler(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<ErrorOr<ClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        if (_clientsRepository.FindByName(request.Name) != null) return ClientErrors.AlreadyExist(request.Name);

        Client client = Client.Create(
            Guid.NewGuid(),
            null,
            request.Name,
            request.Sector,
            request.EmailAddress,
            DateTime.UtcNow
            );

        _clientsRepository.Add(client);

        return await Task.FromResult(
            new ClientResponse(
                client.Id, 
                client.LogoUrl, 
                client.Name, 
                client.Sector, 
                client.EmailAddress, 
                client.CreatedAtUtc)
            );
    }
}
