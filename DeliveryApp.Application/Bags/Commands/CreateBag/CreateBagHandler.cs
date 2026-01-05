using DeliveryApp.Application.Bags.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Bag;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Bags.Commands.CreateBag;

public record CreateBagCommand(Guid UserId, IEnumerable<Guid> FoodsIds) : IRequest<Guid>;

public class CreateBagHandler(IBagRepository bagRepository, IUserRepository userRepository)
    : IRequestHandler<CreateBagCommand, Guid>
{
    public async Task<Guid> Handle(CreateBagCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.UserExists(request.UserId, cancellationToken);
        if (!user) throw new NotFoundException("User not found");

        var bag = new Bag
        {
            FoodsIds = request.FoodsIds,
            UserId = request.UserId,
        };

        await bagRepository.Add(bag, cancellationToken);
        await bagRepository.SaveChanges(cancellationToken);

        return bag.Id;
    }
}