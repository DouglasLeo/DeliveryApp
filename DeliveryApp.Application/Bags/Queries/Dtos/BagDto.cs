namespace DeliveryApp.Application.Bags.Queries.Dtos;

public record BagDto(Guid Id, IEnumerable<BagItemDto> BagItems, decimal TotalPrice);