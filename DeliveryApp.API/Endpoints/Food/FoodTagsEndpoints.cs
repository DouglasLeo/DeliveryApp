using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Foods.Commands.CreateTag;
using DeliveryApp.Application.Foods.Commands.DeleteTag;
using DeliveryApp.Application.Foods.Queries.DTOs;
using DeliveryApp.Application.Foods.Queries.GetTags;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Food;

public class FoodTagsEndpoints : EndpointGroupBase
{
    private const string GroupName = "tags";

    public override void Map(WebApplication app)
    {
        app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllTags)
            .MapPost(CreateTag)
            .MapDelete(DeleteTag, "{id:guid}");
    }

    /// <summary>
    /// Get a paginated list of tags
    /// </summary>
    /// <param name="query">The number of page and page size</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A paginated list of tags</returns>
    public async Task<Results<Ok<PaginatedList<TagDto>>, ValidationProblem>> GetAllTags(
        [AsParameters] GetAllTagsQuery query,
        ISender sender,
        IValidator<GetAllTagsQuery> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query, cancellationToken));
    }

    /// <summary>
    /// Create a tag
    /// </summary>
    /// <param name="command">The tag data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The identifier of the tag</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateTag(
        [FromBody] CreateTagCommand command,
        ISender sender,
        IValidator<CreateTagCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{result}", result);
    }

    /// <summary>
    /// Delete a tag
    /// </summary>
    /// <param name="id">The tag identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoContent> DeleteTag(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTagCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}