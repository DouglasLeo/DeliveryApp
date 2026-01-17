using DeliveryApp.Application.Adresses.Queries;
using DeliveryApp.Domain.Entities.Address;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Common.Mappings;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class AddressDtoMapperly
{
    public static partial AddressDto FromEntity(Address model);
    public static partial IQueryable<AddressDto> ProjectToModel(this IQueryable<Address> model);
}