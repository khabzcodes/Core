using Core.Application.Clients.Common;
using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ErrorOr<ClientResponse>>
{
    private readonly IClientsRepository _clientsRepository;

    public UpdateClientCommandHandler(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<ErrorOr<ClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        if (_clientsRepository.FindByName(request.Name) != null) return ClientErrors.AlreadyExist(request.Name);

        Client? client = _clientsRepository.FindById(request.Id);
        if (client == null) return ClientErrors.NotFound;

        client.Name = request.Name;
        client.Sector = request.Sector;
        client.EmailAddress = request.EmailAddress;

        Client updateClient = _clientsRepository.Update(client);

        return await Task.FromResult(new ClientResponse(
            updateClient.Id, 
            updateClient.LogoUrl, 
            updateClient.Name, 
            updateClient.Sector, 
            updateClient.EmailAddress, 
            updateClient.CreatedAtUtc));
    }
}
