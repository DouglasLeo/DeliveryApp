using DeliveryApp.Domain.Entities.Food;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Foods.Queries.DTOs;

public record TagDto(Guid Id, string Name);

