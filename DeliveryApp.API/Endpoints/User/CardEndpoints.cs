using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Users.Commands.CreateCard;
using DeliveryApp.Application.Users.Commands.DeleteCard;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Application.Users.Queries.GetCard;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.User;

public class CardEndpoints : EndpointGroupBase
{
    private const string GroupName = "cards";

    public override void Map(WebApplication app)
    {
        app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllCardsByUser, "user/{userId:guid}")
            .MapGet(GetCardById, "{id:guid}")
            .MapPost(CreateCard)
            .MapDelete(DeleteCard, "{id:guid}");
    }

    /// <summary>
    /// Get all the cards of user
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A user cards list</returns>
    public async Task<Ok<IEnumerable<CardDto>>> GetAllCardsByUser(
        [FromRoute] Guid userId,
        ISender sender, 
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetAllCardsByUserIdQuery(userId), cancellationToken));

    /// <summary>
    /// Get a card
    /// </summary>
    /// <param name="id">The card identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A card data</returns>
    public async Task<Ok<CardDto>> GetCardById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCardByIdQuery(id), cancellationToken);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Create a card
    /// </summary>
    /// <param name="command">The card data </param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The card identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateCard(
        [FromBody] CreateCardCommand command,
        ISender sender,
        IValidator<CreateCardCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{id}", id);
    }

    /// <summary>
    /// Delete a card
    /// </summary>
    /// <param name="id">The card identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoContent> DeleteCard(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCardCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}