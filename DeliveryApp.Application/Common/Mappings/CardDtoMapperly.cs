using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Common.Mappings;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class CardDtoMapperly
{
    public static partial IQueryable<CardDto> ProjectToModel(this IQueryable<Card> model);
}