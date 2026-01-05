using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Domain.Exceptions;
using MediatR;

namespace DeliveryApp.Application.Foods.Commands.DeleteTag;

public record DeleteTagCommand(Guid Id) : IRequest<Guid>;

public class DeleteTagHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand, Guid>
{
    public async Task<Guid> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.FindById(request.Id, cancellationToken) ??
                  throw new NotFoundException("Tag not found.");

        await tagRepository.Remove(tag, cancellationToken);
        await tagRepository.SaveChanges(cancellationToken);

        return tag.Id;
    }
}