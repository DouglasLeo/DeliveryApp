namespace DeliveryApp.Domain.Exceptions;

public class LimitExceededException(string type, int quantity)
    : Exception($"You have exceeded the maximum number of {type}, limit is {quantity}.");