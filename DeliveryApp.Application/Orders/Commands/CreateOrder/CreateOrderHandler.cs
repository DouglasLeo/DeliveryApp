using DeliveryApp.Application.Adresses.Abstractions;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Domain.Enums;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    EOrderStatus OrderStatus,
    IEnumerable<Guid> ItemsIds,
    EPaymentMethod PaymentMethod) : IRequest<Guid>;

public class CreateOrderHandler(
    IOrderRepository orderRepository,
    IOrderItemsRepository orderItemsRepository,
    IFoodRepository foodRepository,
    IUserRepository userRepository,
    IAddressRepository addressRepository,
    ICardRepository cardRepository) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.UserId, cancellationToken);
        if (user is null)
            throw new NotFoundException("User not found");

        var foodItems = (await foodRepository.GetFoodsByIds(request.ItemsIds, cancellationToken)).ToList();
        if (foodItems.Count == 0)
            throw new NotFoundException("Foods not found");

        if (user.AddressId is null) throw new NotDefinedException("Address");

        var address = await addressRepository.FindById(user.AddressId.Value, cancellationToken) ??
                      throw new NotFoundException("User Address not found");
        
        if (user.CardId is null) throw new NotDefinedException("Card");
        
        var card = await cardRepository.FindById(user.AddressId.Value, cancellationToken) ??
                      throw new NotFoundException("User card not found");
        
        var order = Order.Create(request.UserId, request.OrderStatus, request.PaymentMethod, card, address);
        var orderItems = OrderItems.Create(foodItems, order.Id);

        await orderRepository.Add(order, cancellationToken);
        await orderItemsRepository.AddRange(orderItems, cancellationToken);

        await orderRepository.SaveChanges(cancellationToken);

        return order.Id;
    }
}