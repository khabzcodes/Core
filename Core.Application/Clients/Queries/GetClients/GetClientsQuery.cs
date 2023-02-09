using Core.Application.Clients.Common;
using Core.Application.Common.Response;
using ErrorOr;
using MediatR;

namespace Core.Application.Clients.Queries.GetClients;

public record GetClientsQuery(int PageNumber, int PageSize) : IRequest<ErrorOr<PaginatedResponse<List<ClientResponse>>>>;
