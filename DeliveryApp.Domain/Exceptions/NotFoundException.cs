namespace DeliveryApp.Domain.Exceptions;

public class NotFoundException(string message) : DomainException(message);