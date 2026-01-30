using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Extensions;

public static class ProblemResults
{
    public static BadRequest<ProblemDetails> IdMismatch(Guid routeId, Guid bodyId)
        => TypedResults.BadRequest(new ProblemDetails
        {
            Title = "Invalid request",
            Detail = $"Route id '{routeId}' must match body id '{bodyId}'.",
            Status = StatusCodes.Status400BadRequest
        });
}