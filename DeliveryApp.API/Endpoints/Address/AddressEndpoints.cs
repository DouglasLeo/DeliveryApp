using DeliveryApp.API.Extensions;
using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Adresses.Commands.CreateAddress;
using DeliveryApp.Application.Adresses.Commands.DeleteAddress;
using DeliveryApp.Application.Adresses.Commands.UpdateAddress;
using DeliveryApp.Application.Adresses.Queries;
using DeliveryApp.Application.Adresses.Queries.GetAddress;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Address;

public class AddressEndpoints : EndpointGroupBase
{
    private const string GroupName = "addresses";

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(GroupName);
        //.RequireAuthorization()
        group.MapGet(GetAllAddressesByUser, "user/{userId:guid}");
        group.MapGet(GetAddressById, "{id:guid}");
        group.MapPost(CreateAddress);
        group.MapPut(UpdateAddress, "{id:guid}");
        group.MapDelete(DeleteAddress, "{id:guid}");
    }

    /// <summary>
    /// Get all user addresses
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>All addresses of the user</returns>
    public async Task<Ok<IEnumerable<AddressDto>>> GetAllAddressesByUser(
        [FromRoute] Guid userId,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetAddressesByUserIdQuery(userId), cancellationToken));

    /// <summary>
    /// Get address data
    /// </summary>
    /// <param name="id">The address identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The address data</returns>
    public async Task<Ok<AddressDto>> GetAddressById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetAddressByIdQuery(id), cancellationToken));

    /// <summary>
    /// Create an address
    /// </summary>
    /// <param name="command">The address data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The address identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateAddress(
        [FromBody] CreateAddressCommand command,
        ISender sender,
        IValidator<CreateAddressCommand> validator, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{id}", id);
    }

    /// <summary>
    /// Update the data of an address
    /// </summary>
    /// <param name="id">The address identifier</param>
    /// <param name="command">The address data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The address identifier</returns>
    public async Task<Results<NoContent, ValidationProblem, BadRequest<ProblemDetails>>> UpdateAddress(
        [FromRoute] Guid id,
        [FromBody] UpdateAddressCommand command,
        ISender sender,
        IValidator<UpdateAddressCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return ProblemResults.IdMismatch(id, command.Id);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);

        return TypedResults.NoContent();
    }

    /// <summary>
    /// Delete an address
    /// </summary>
    /// <param name="id">The address identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoContent> DeleteAddress(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteAddressCommand(id), cancellationToken);

        return TypedResults.NoContent();
    }
}