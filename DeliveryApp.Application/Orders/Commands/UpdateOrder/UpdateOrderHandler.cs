using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(Guid Id, Guid UserId, EOrderStatus OrderStatus) : IRequest<Guid>;

public class UpdateOrderHandler(
    IOrderRepository orderRepository,
    IUserRepository userRepository) : IRequestHandler<UpdateOrderCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.UserExists(request.UserId, cancellationToken);
        if (!user)
            throw new NotFoundException("User not found");

        var order = await orderRepository.FindById(request.Id, cancellationToken) ??
                    throw new NotFoundException("Order not found");
        
        order.Update(request.OrderStatus);

        await orderRepository.Update(order, cancellationToken);
        await orderRepository.SaveChanges(cancellationToken);

        return order.Id;
    }
}