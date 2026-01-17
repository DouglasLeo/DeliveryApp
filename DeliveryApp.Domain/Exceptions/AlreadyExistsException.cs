namespace DeliveryApp.Domain.Exceptions;

public class AlreadyExistsException(string message) : Exception(message);