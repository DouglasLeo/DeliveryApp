namespace DeliveryApp.Application.Users.Queries.Dtos;

public record UserDto(Guid Id, string Name, string Email, bool Active);