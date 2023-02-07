using Core.Application.Clients.Common;
using Core.Application.Persistence;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ErrorOr<List<ClientResponse>>>
{
    private readonly IClientsRepository _clientsRepository;

    public GetClientsQueryHandler(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<ErrorOr<List<ClientResponse>>> Handle(GetClientsQuery query, CancellationToken cancellationToken)
    {
        List<Client> clients = _clientsRepository.FindAll();

        List<ClientResponse> results = clients
            .Select(c => new ClientResponse(
                c.Id, 
                c.LogoUrl, 
                c.Name, 
                c.Sector, 
                c.EmailAddress, 
                c.CreatedAtUtc))
            .OrderBy(x => x.Name)
            .Distinct()
            .ToList();
        
        return await Task.FromResult(results);
    }
}
