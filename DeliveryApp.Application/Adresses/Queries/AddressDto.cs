namespace DeliveryApp.Application.Adresses.Queries;

public record AddressDto(string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Neighboorhood,
    string Country,
    string? Reference);
    
