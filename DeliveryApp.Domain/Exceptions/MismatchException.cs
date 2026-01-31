namespace DeliveryApp.Domain.Exceptions;

public class MismatchException(string childEntity, string parentEntity) : DomainException($"The {childEntity} does not belong to the specified {parentEntity}.");