namespace DeliveryApp.Domain.Exceptions;

public class NotDefinedException(string type) : Exception($"{type} for user is not defined.");