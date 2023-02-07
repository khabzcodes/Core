using Core.Application.Clients.Common;
using Core.Application.Common.Errors;
using Core.Application.Persistence;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClient;

public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ErrorOr<ClientResponse>>
{
    private readonly IClientsRepository _clientsRepository;

    public GetClientQueryHandler(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<ErrorOr<ClientResponse>> Handle(GetClientQuery query, CancellationToken cancellationToken)
    {
        Client? client = _clientsRepository.FindById(query.Id);

        if (client == null) return ClientErrors.NotFound;

        ClientResponse results = new(client.Id, client.LogoUrl, client.Name, client.Sector, client.EmailAddress, client.CreatedAtUtc);

        return await Task.FromResult(results);
    }
}
