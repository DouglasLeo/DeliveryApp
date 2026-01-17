using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Adresses.Queries;
using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Domain.Entities.Address;
using DeliveryApp.Infrastructure.Persistence.Shared;
using DeliveryApp.Infrastructure.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infrastructure.Persistence.Adresses.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AddressDto>> FindAllByUserId(Guid userId, CancellationToken cancellationToken)
        => await DbSet.AsNoTracking().Where(a => a.UserId == userId).ProjectToModel().ToListAsync(cancellationToken);
}