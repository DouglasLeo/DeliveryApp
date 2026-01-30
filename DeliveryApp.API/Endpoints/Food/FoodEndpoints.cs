using DeliveryApp.API.Extensions;
using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Foods.Commands.CreateFood;
using DeliveryApp.Application.Foods.Commands.UpdateFood;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Application.Foods.Queries.GetFoods;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Food;

public class FoodEndpoints : EndpointGroupBase
{
    private const string GroupName = "foods";

    public override void Map(WebApplication app)
    {
        app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllFoods)
            .MapGet(GetAllFoodsWithInactives, "/admin")
            .MapGet(GetFoodById, "{id:guid}")
            .MapGet(GetFoodByIdWithInactives, "/admin/{id:guid}")
            .MapPost(CreateFood)
            .MapPut(UpdateFood, "{id:guid}");
    }

    /// <summary>
    /// Get a list of all foods
    /// </summary>
    /// <param name="query">The number to skip and the number to take</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A foods list</returns>
    public async Task<Results<Ok<IEnumerable<FoodDto>>, ValidationProblem>> GetAllFoods(
        [AsParameters] GetAllFoodsQuery query,
        ISender sender,
        IValidator<GetAllFoodsQuery> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Get a list of all foods including inactives
    /// </summary>
    /// <param name="query">The number to skip and the number to take</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A foods list</returns>
    public async Task<Results<Ok<IEnumerable<FoodDto>>, ValidationProblem>> GetAllFoodsWithInactives(
        [AsParameters] GetAllFoodsWithInactivesQuery query,
        ISender sender,
        IValidator<GetAllFoodsWithInactivesQuery> validator,
        CancellationToken cancellationToken)
    {
        // only admin
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Get a food
    /// </summary>
    /// <param name="id">The food identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A food data</returns>
    public async Task<Ok<FoodDto>> GetFoodById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetFoodByIdQuery(id), cancellationToken));

    /// <summary>
    /// Get a food including inactives
    /// </summary>
    /// <param name="id">The food identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A food data</returns>
    public async Task<Ok<FoodDto>> GetFoodByIdWithInactives(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetFoodByIdWithInactivesQuery(id), cancellationToken));

    /// <summary>
    /// Create a food
    /// </summary>
    /// <param name="command">The food data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The food identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateFood(
        [FromBody] CreateFoodCommand command,
        ISender sender,
        IValidator<CreateFoodCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{result}", result);
    }

    /// <summary>
    /// Update a food
    /// </summary>
    /// <param name="id">The food identifier</param>
    /// <param name="command">The food data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, BadRequest<ProblemDetails>, ValidationProblem>> UpdateFood(
        [FromRoute] Guid id,
        [FromBody] UpdateFoodCommand command,
        ISender sender,
        IValidator<UpdateFoodCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return ProblemResults.IdMismatch(id, command.Id);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);

        return TypedResults.NoContent();
    }
}