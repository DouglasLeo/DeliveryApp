namespace DeliveryApp.Application.Users.Queries;

public record UserDto(Guid Id, string Name, string Email, string[] Roles);