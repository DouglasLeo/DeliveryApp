namespace DeliveryApp.Domain.Exceptions;

public class UserMismatchException(string obj) : Exception($"The {obj} does not belong to the specified user.");