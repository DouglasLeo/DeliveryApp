using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Commands.UpdateBag;

public record UpdateBagCommand(Guid Id, IEnumerable<Guid> FoodsIds) : IRequest<Guid>;

public class UpdateBagHandler(IBagRepository bagRepository) : IRequestHandler<UpdateBagCommand, Guid>
{
    public async Task<Guid> Handle(UpdateBagCommand request, CancellationToken cancellationToken)
    {
        var bag = await bagRepository.FindById(request.Id, cancellationToken) ??
                  throw new NotFoundException("Bag not found");

        bag.FoodsIds = request.FoodsIds;

        await bagRepository.Update(bag, cancellationToken);
        await bagRepository.SaveChanges(cancellationToken);

        return bag.Id;
    }
}