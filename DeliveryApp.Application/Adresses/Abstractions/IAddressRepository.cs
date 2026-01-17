using DeliveryApp.Application.Adresses.Queries;
using DeliveryApp.Application.Shared.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Address;
using DeliveryApp.Domain.Entities.Shared;

namespace DeliveryApp.Application.Adresses.Abstractions;

public interface IAddressRepository : IRepository<Address>
{
    Task<IEnumerable<AddressDto>> FindAllByUserId(Guid userId, CancellationToken cancellationToken);
}