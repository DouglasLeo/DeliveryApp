using DeliveryApp.Application.Foods.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    public Task<string> SaveImageAsync(IFormFile image, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}