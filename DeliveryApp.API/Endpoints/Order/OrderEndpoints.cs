using DeliveryApp.API.Extensions;
using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Orders.Commands.CreateOrder;
using DeliveryApp.Application.Orders.Commands.UpdateOrder;
using DeliveryApp.Application.Orders.Queries;
using DeliveryApp.Application.Orders.Queries.GetOrder;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.Order;

public class OrderEndpoints : EndpointGroupBase
{
    private const string GroupName = "orders";

    public override void Map(WebApplication app)
    {
        app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllOrdersByUserId, "/user/{userId:guid}")
            .MapGet(GetOrdersById, "{Id:guid}")
            .MapPost(CreateOrder)
            .MapPut(UpdateOrder, "{id:guid}");
    }

    /// <summary>
    /// Get a list of user orders
    /// </summary>
    /// <param name="userId">The user identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A user orders list</returns>
    public async Task<Results<Ok<IEnumerable<OrderDto>>, ValidationProblem>> GetAllOrdersByUserId(
        [FromRoute] Guid userId,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetAllOrderByUserIdQuery(userId), cancellationToken));
    
    /// <summary>
    /// Get an order
    /// </summary>
    /// <param name="id">The order identifier</param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An order data</returns>
    public async Task<Ok<OrderDto>> GetOrdersById(
        [FromRoute] Guid id,
        ISender sender,
        CancellationToken cancellationToken) =>
        TypedResults.Ok(await sender.Send(new GetOrderByIdQuery(id), cancellationToken));

    /// <summary>
    /// Create an order
    /// </summary>
    /// <param name="command">The order data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The order identifier</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateOrder(
        [FromBody] CreateOrderCommand command,
        ISender sender,
        IValidator<CreateOrderCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(command, cancellationToken);
        
        return TypedResults.Created($"/{GroupName}/{result}", result);
    }
    
    /// <summary>
    /// Update an order
    /// </summary>
    /// <param name="id">The order identifier</param>
    /// <param name="command">The order data</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, ValidationProblem, BadRequest<ProblemDetails>>> UpdateOrder(
        [FromRoute] Guid id,
        [FromBody] UpdateOrderCommand command,
        ISender sender,
        IValidator<UpdateOrderCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return ProblemResults.IdMismatch(id, command.Id);
        
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);
        
        return TypedResults.NoContent();
    }
}