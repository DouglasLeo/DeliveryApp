using DeliveryApp.Application.Common.Mappings;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Foods.Abstractions.Repositories;
using DeliveryApp.Application.Foods.Queries.DTOs;
using MediatR;

namespace DeliveryApp.Application.Foods.Queries.GetFoodCategories;

public record GetAllFoodCategoriesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<FoodCategoryDto>>;

public class GetAllFoodCategoriesHandler(IFoodCategoryRepository foodCategoryRepository)
    : IRequestHandler<GetAllFoodCategoriesQuery, PaginatedList<FoodCategoryDto>>
{
    public async Task<PaginatedList<FoodCategoryDto>> Handle(GetAllFoodCategoriesQuery request,
        CancellationToken cancellationToken)
        => await foodCategoryRepository.GetAllFoodCategories(request.PageNumber, request.PageSize, cancellationToken)
            .ProjectToModel()
            .PaginatedListAsync(request.PageNumber, request.PageSize);
}