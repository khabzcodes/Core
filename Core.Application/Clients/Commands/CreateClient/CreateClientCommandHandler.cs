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
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(
        IClientsRepository clientsRepository, 
        IUnitOfWork unitOfWork)
    {
        _clientsRepository = clientsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<ClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        if (_clientsRepository.FindByName(request.Name) is not null)
        {
            return ClientErrors.AlreadyExist(request.Name);
        }

        Client client = Client.Create(
            Guid.NewGuid(),
            null,
            request.Name,
            request.Sector,
            request.EmailAddress,
            DateTime.UtcNow
            );

        await _clientsRepository.AddAsync(client);

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
