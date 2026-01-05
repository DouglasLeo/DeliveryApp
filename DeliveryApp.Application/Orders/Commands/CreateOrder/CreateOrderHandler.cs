using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid UserId, EOrderStatus OrderStatus, IEnumerable<Guid> ItemsIds) : IRequest<Guid>;

public class CreateOrderHandler(
    IOrderRepository orderRepository,
    IOrderItemsRepository orderItemsRepository,
    IFoodRepository foodRepository,
    IUserRepository userRepository) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.UserExists(request.UserId, cancellationToken);
        if (!user)
            throw new NotFoundException("User not found");

        var foodItems = (await foodRepository.GetFoodsByIds(request.ItemsIds)).ToList();
        if (foodItems.Count == 0)
            throw new NotFoundException("Foods not found");

        var order = new Order { UserId = request.UserId, EOrderStatus = request.OrderStatus };
        var orderItems = OrderItems.Create(foodItems, order.Id);

        await orderRepository.Add(order, cancellationToken);
        await orderItemsRepository.AddRange(orderItems, cancellationToken);

        await orderRepository.SaveChanges(cancellationToken);

        return order.Id;
    }
}