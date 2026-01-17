using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Foods.Abstractions.Repositories;

public interface IFileStorageService
{
    Task<string> SaveImageAsync(IFormFile image, CancellationToken cancellationToken);
}