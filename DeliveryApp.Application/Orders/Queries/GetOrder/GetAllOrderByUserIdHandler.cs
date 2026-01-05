using DeliveryApp.Application.Orders.Abstractions.Repositories;
using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Orders.Queries.GetOrder;

public record GetAllOrderByUserIdQuery(Guid UserId) : IRequest<IEnumerable<OrderDto>>;

public class GetAllOrderByUserIdHandler(IOrderRepository orderRepository, IUserRepository userRepository)
    : IRequestHandler<GetAllOrderByUserIdQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.UserExists(request.UserId, cancellationToken);
        if (!user)
            throw new NotFoundException("User not found");

        return await orderRepository.FindAllOrdersById(request.UserId, cancellationToken);
    }
}