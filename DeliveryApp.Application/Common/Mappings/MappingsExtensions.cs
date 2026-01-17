using DeliveryApp.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Application.Common.Mappings;

public static class MappingsExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);
}