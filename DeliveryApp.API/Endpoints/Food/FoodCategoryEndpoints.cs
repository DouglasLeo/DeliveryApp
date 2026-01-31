using DeliveryApp.API.Extensions;
using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Foods.Commands.CreateFoodCategory;
using DeliveryApp.Application.Foods.Commands.UpdateFoodCategory;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Application.Foods.Queries.GetFoodCategories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Food;

public class FoodCategoryEndpoints : EndpointGroupBase
{
    private const string GroupName = "foodcategories";

    public override void Map(WebApplication app)
    {
        app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllFoodCategories)
            .MapPost(CreateFoodCategory)
            .MapPut(UpdateFoodCategory, "{id:guid}");
    }

    /// <summary>
    /// Get a paginated list of food categories
    /// </summary>
    /// <param name="query">The number of page and page size</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of food categories</returns>
    public async Task<Results<Ok<PaginatedList<FoodCategoryDto>>, ValidationProblem>> GetAllFoodCategories(
        [AsParameters] GetAllFoodCategoriesQuery query,
        ISender sender,
        IValidator<GetAllFoodCategoriesQuery> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Create a food category
    /// </summary>
    /// <param name="command">The food category data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The food category identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateFoodCategory(
        [FromBody] CreateFoodCategoryCommand command,
        ISender sender,
        IValidator<CreateFoodCategoryCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{result}", result);
    }

    /// <summary>
    /// Update a food category
    /// </summary>
    /// <param name="id">The food category identifier </param>
    /// <param name="command">The food category data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, ValidationProblem, BadRequest<ProblemDetails>>> UpdateFoodCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateFoodCategoryCommand command,
        ISender sender,
        IValidator<UpdateFoodCategoryCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return ProblemResults.IdMismatch(id, command.Id);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);

        return TypedResults.NoContent();
    }
}