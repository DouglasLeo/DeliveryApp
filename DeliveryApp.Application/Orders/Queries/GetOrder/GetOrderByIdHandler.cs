using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Orders.Queries.GetOrder;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto>;

public class GetOrderByIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        => await orderRepository.FindOrderById(request.Id, cancellationToken) ?? throw new NotFoundException($"Order with id {request.Id} not found");
}