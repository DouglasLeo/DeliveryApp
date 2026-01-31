using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Bag;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Commands.CreateBag;

public record CreateBagCommand(Guid UserId, Dictionary<Guid, int> Items) : IRequest<Guid>;

public class CreateBagHandler(IBagRepository bagRepository, IUserRepository userRepository)
    : IRequestHandler<CreateBagCommand, Guid>
{
    public async Task<Guid> Handle(CreateBagCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.UserExists(request.UserId, cancellationToken);
        if (!user) throw new NotFoundException("User not found");

        var existingBag = await bagRepository.FindBagByUserId(request.UserId, cancellationToken);

        var bag = Bag.CreateOrUpdate(existingBag, request.UserId, request.Items);

        await bagRepository.UpsertBag(bag, cancellationToken);

        return bag.Id;
    }
}