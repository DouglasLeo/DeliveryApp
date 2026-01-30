namespace DeliveryApp.Domain.Exceptions;

public class NotDefinedException(string type) : DomainException($"{type} for user is not defined.");