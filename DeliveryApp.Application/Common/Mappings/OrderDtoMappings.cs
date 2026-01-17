using System.Linq.Expressions;
using DeliveryApp.Application.Adresses.Queries;
using DeliveryApp.Application.Orders.Queries;
using DeliveryApp.Domain.Entities.Order;

namespace DeliveryApp.Application.Common.Mappings;

public static class OrderDtoMappings
{
    public static Expression<Func<Order, OrderDto>> ToDtoExpression() =>
        order => new OrderDto(order.Id, order.OrderStatus, order.CreatedAt.DateTime,
            order.OrderItems.Select(oi => oi.Food.ToDto()),
            new AddressDto(order.Street, order.HouseNumber, order.PostalCode, order.City, order.Neighboorhood,
                order.Country, order.Reference));

    public static OrderDto ToDto(this Order order) => new(order.Id, order.OrderStatus, order.CreatedAt.DateTime,
        order.OrderItems.Select(oi => oi.Food.ToDto()),
        new AddressDto(order.Street, order.HouseNumber, order.PostalCode, order.City, order.Neighboorhood,
            order.Country, order.Reference));
    
    public static IQueryable<OrderDto> ToDto(this IQueryable<Order> orders) => orders.Select(ToDtoExpression());
}