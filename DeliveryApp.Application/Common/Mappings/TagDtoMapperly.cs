using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Domain.Entities.Food;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Common.Mappings;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class TagDtoMapperly
{
    public static partial TagDto FromEntity(Tag model);
    public static partial IQueryable<TagDto> ProjectToModel(this IQueryable<Tag> model);
}