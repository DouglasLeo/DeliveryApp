using DeliveryApp.API.Extensions;
using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Bags.Commands.CreateBag;
using DeliveryApp.Application.Bags.Commands.DeleteBag;
using DeliveryApp.Application.Bags.Commands.UpdateBag;
using DeliveryApp.Application.Bags.Queries.Dtos;
using DeliveryApp.Application.Bags.Queries.GetBag;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Bag;

public class BagEndpoints : EndpointGroupBase
{
    private const string GroupName = "bags";
    
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(GroupName);
        //.RequireAuthorization()
        group.MapGet(GetBagByUserId, "user/{userId:guid}");
        group.MapGet(GetBagById, "{id:guid}");
        group.MapPost(CreateBag);
        group.MapPut(UpdateBag, "{id:guid}");
        group.MapDelete(DeleteBag, "{id:guid}");
    }
    
    /// <summary>
    /// Get the bag of user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The bag data</returns>
    public async Task<Ok<BagDto>> GetBagByUserId(
        [FromRoute] Guid userId,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetBagByUserIdQuery(userId), cancellationToken));
    
    /// <summary>
    /// Get a bag
    /// </summary>
    /// <param name="id">The bag identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The bag data</returns>
    public async Task<Ok<BagDto>> GetBagById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetBagByIdQuery(id), cancellationToken));
    
    /// <summary>
    /// Create a bag
    /// </summary>
    /// <param name="command">The bag data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The bag identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateBag(
        [FromBody] CreateBagCommand command,
        ISender sender,
        IValidator<CreateBagCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{id}", id);
    }
    
    /// <summary>
    /// Update a bag
    /// </summary>
    /// <param name="id">The bag identifier</param>
    /// <param name="command">The bag data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, ValidationProblem, BadRequest<ProblemDetails>>> UpdateBag(
        [FromRoute] Guid id,
        [FromBody] UpdateBagCommand command,
        ISender sender,
        IValidator<UpdateBagCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return ProblemResults.IdMismatch(id, command.Id);
        
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);
        
        return TypedResults.NoContent();
    }
    
    /// <summary>
    /// Delete a bag
    /// </summary>
    /// <param name="id">The bag identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoContent> DeleteBag(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteBagCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}