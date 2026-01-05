using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Commands.DeleteBag;

public record DeleteBagCommand(Guid Id) : IRequest<Guid>;

public class DeleteBagHandler(IBagRepository bagRepository) : IRequestHandler<DeleteBagCommand, Guid>
{
    public async Task<Guid> Handle(DeleteBagCommand request, CancellationToken cancellationToken)
    {
        var bag = await bagRepository.FindById(request.Id, cancellationToken) ??
                  throw new NotFoundException("Bag not found");

        await bagRepository.Remove(bag, cancellationToken);
        await bagRepository.SaveChanges(cancellationToken);

        return bag.Id;
    }
}