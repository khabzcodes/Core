using Core.Application.Clients.Common;
using Core.Application.Common.Response;
using Core.Application.Persistence;
using Core.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, ErrorOr<PaginatedResponse<List<ClientResponse>>>>
{
    private readonly IClientsRepository _clientsRepository;

    public GetClientsQueryHandler(IClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }

    public async Task<ErrorOr<PaginatedResponse<List<ClientResponse>>>> Handle(GetClientsQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Client> clients = await _clientsRepository.GetAllAsync();

        List<ClientResponse> clientResponse = clients
            .OrderBy(x => x.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new ClientResponse(
                c.Id, 
                c.LogoUrl, 
                c.Name, 
                c.Sector, 
                c.EmailAddress, 
                c.CreatedAtUtc))
            .ToList();

        int totalPages = (int)Math.Ceiling(clients.Count() / (double)query.PageSize);

        PaginatedResponse<List<ClientResponse>> results = new(
            clientResponse, 
            query.PageNumber, 
            totalPages, 
            clients.Count(), 
            query.PageNumber > 1, 
            query.PageNumber < totalPages); 

        return await Task.FromResult(results);
    }
}
