namespace Core.Application.Common.Response;

public record PaginatedResponse<T>(
    T Data,
    int CurrentPage,
    int TotalPages,
    int TotalRecords,
    bool HasPrevious,
    bool HasNext
    );
