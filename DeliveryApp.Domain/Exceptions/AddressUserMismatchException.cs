namespace DeliveryApp.Domain.Exceptions;

public class AddressUserMismatchException() : Exception("The address does not belong to the specified user.");