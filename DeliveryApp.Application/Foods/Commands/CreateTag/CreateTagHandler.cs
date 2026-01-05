using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.CreateTag;

public record CreateTagCommand(string Name) : IRequest<Guid>;

public class CreateTagHandler(ITagRepository tagRepository) : IRequestHandler<CreateTagCommand, Guid>
{
    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.FindByName(request.Name, cancellationToken);

        if (tag is not null) throw new AlreadyExistsException($"Tag with name {request.Name} already exists.");

        var tagEntity = new Tag
        {
            Name = request.Name
        };

        await tagRepository.Add(tagEntity, cancellationToken);
        await tagRepository.SaveChanges(cancellationToken);

        return tagEntity.Id;
    }
}