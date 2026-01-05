using DeliveryApp.Domain.Entities.Food;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Foods.Queries.DTOs;

public record TagDto(Guid Id, string Name);

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class TagDtoMapper
{
    public static partial IQueryable<TagDto> ProjectToModel(this IQueryable<Tag> model);
}