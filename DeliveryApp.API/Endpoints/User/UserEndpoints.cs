using DeliveryApp.API.Infrastructure;
using DeliveryApp.Application.Common.Models;
using DeliveryApp.Application.Users.Commands.CreateUser;
using DeliveryApp.Application.Users.Commands.DeleteUser;
using DeliveryApp.Application.Users.Commands.UpdateUser;
using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Application.Users.Queries.GetUser;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Endpoints.User;

public class UserEndpoints : EndpointGroupBase
{
    private const string GroupName = "users";

    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(GroupName)
            //.RequireAuthorization()
            .MapGet(GetAllUsers)
            .MapGet(GetUserById, "{id:guid}")
            .MapGet(GetUserByEmail, "{email}")
            .MapPost(CreateUser)
            .MapPut(UpdateUser, "{id:guid}")
            .MapDelete(DeleteUser, "{id:guid}");
    }

    /// <summary>
    /// Get a paginated list of all users
    /// </summary>
    /// <param name="query">Query of users</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <returns>A paginated list of all users</returns>
    public async Task<Results<Ok<PaginatedList<UserDto>>, ValidationProblem>> GetAllUsers(
        [AsParameters] GetAllUsersQuery query,
        ISender sender,
        IValidator<GetAllUsersQuery> validator)
    {
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return TypedResults.Ok(await sender.Send(query));
    }

    /// <summary>
    /// Get the user of the specified identifier
    /// </summary>
    /// <param name="id">Identifier of the user</param>
    /// <param name="sender"></param>
    /// <returns>The requested user data</returns>
    public async Task<Ok<UserDto>> GetUserById(
        [FromRoute] Guid id,
        ISender sender)
    {
        var result = await sender.Send(new GetUserByIdQuery(id));

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Get the user of the specified email
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <returns>The requested user data</returns>
    public async Task<Results<Ok<UserDto>, ValidationProblem>> GetUserByEmail(
        [FromRoute] string email,
        ISender sender,
        IValidator<GetUserByEmailQuery> validator)
    {
        var query = new GetUserByEmailQuery(email);
        var validationResult = await validator.ValidateAsync(query);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="command">Data of user to be created</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The identifier of created user</returns>
    public async Task<Results<Created<Guid>, ValidationProblem>> CreateUser(
        [FromBody] CreateUserCommand command,
        ISender sender,
        IValidator<CreateUserCommand> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var id = await sender.Send(command, cancellationToken);

        return TypedResults.Created($"/{GroupName}/{id}", id);
    }

    /// <summary>
    /// Update the user data
    /// </summary>
    /// <param name="id">Identifier of user</param>
    /// <param name="command">New data of the user</param>
    /// <param name="sender"></param>
    /// <param name="validator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, NotFound, BadRequest, ValidationProblem>> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UpdateUserCommand command,
        ISender sender,
        IValidator<UpdateUserCommand> validator,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) return TypedResults.ValidationProblem(validationResult.ToDictionary());

        await sender.Send(command, cancellationToken);

        return TypedResults.NoContent();
    }

    /// <summary>
    /// Delete the user
    /// </summary>
    /// <param name="id">Identifier of the user</param>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Results<NoContent, BadRequest>> DeleteUser(
        [FromRoute] Guid id,
        [FromBody] DeleteUserCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command, cancellationToken);

        return TypedResults.NoContent();
    }
}