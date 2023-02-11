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
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IClientsRepository clientsRepository, IUnitOfWork unitOfWork)
    {
        _clientsRepository = clientsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        if (_clientsRepository.FindByName(request.Name) is not null) return ClientErrors.AlreadyExist(request.Name);

        Client? client = await _clientsRepository.GetByIdAsync(request.Id);
        if (client is null) return ClientErrors.NotFound;

        client.Name = request.Name;
        client.Sector = request.Sector;
        client.EmailAddress = request.EmailAddress;

        _clientsRepository.Update(client);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ClientResponse(
            client.Id,
            client.LogoUrl,
            client.Name,
            client.Sector,
            client.EmailAddress,
            client.CreatedAtUtc);
    }
}
