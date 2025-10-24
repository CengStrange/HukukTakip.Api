namespace HukukTakip.Api.Application.DTOs;

public record PagedResult<T>(
    int Page,
    int PageSize,
    int TotalCount,
    IEnumerable<T> Items
);
