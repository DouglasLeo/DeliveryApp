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
        var group = app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllCardsByUser)
            .MapGet(GetCardById, "{id:guid}")
            .MapPost(CreateCard)
            .MapDelete(DeleteCard, "{id:guid}");
    }

    /// <summary>
    /// Get all the cards from requested user
    /// </summary>
    /// <param name="id">The identifier of the user</param>
    /// <param name="sender"></param>
    /// <returns>A list of the user's cards</returns>
    
    public async Task<Ok<IEnumerable<CardDto>>> GetAllCardsByUser(
        [FromRoute] Guid id,
        ISender sender) =>
        TypedResults.Ok(await sender.Send(new GetAllCardsByUserIdQuery(id)));
    
    /// <summary>
    /// Get a card data
    /// </summary>
    /// <param name="id">The identifier of card</param>
    /// <param name="sender"></param>
    /// <returns>A card data</returns>
    public async Task<Ok<CardDto>> GetCardById(
        [FromRoute] Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetCardByIdQuery(id));

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Create a card
    /// </summary>
    /// <param name="command">The data of card</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <returns>The identifier of created card</returns>
    public async Task<Results<Ok<Guid>, ValidationProblem>> CreateCard([FromBody] CreateCardCommand command,
        ISender sender,
        IValidator<CreateCardCommand> validator)
    {
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Delete a card
    /// </summary>
    /// <param name="id">The identifier of Card</param>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<NoContent> DeleteCard([FromRoute] Guid id, ISender sender)
    {
        await sender.Send(new DeleteCardCommand(id));

        return TypedResults.NoContent();
    }
}