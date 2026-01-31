using DeliveryApp.Application.Users.Abstractions.Repositories;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Users.Queries.GetCard;

public record GetAllCardsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<CardDto>>;

public class GetAllCardsByUserIdHandler(ICardRepository cardRepository, IUserRepository userRepository)
    : IRequestHandler<GetAllCardsByUserIdQuery, IEnumerable<CardDto>>
{
    public async Task<IEnumerable<CardDto>> Handle(GetAllCardsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userExists = await userRepository.UserExists(request.UserId, cancellationToken);
        if(!userExists) throw new NotFoundException("User not found");
        
        return await cardRepository.FindAllCardsByUserId(request.UserId, cancellationToken);
    }
}