using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetTags;

public record GetAllTagsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<TagDto>>;

public class GetAllFoodCategoriesHandler(ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, PaginatedList<TagDto>>
{
    public async Task<PaginatedList<TagDto>> Handle(GetAllTagsQuery request,
        CancellationToken cancellationToken)
        => await tagRepository.GetAllTags(request.PageNumber, request.PageSize, cancellationToken)
            .ProjectToModel()
            .PaginatedListAsync(request.PageNumber, request.PageSize);
}