using DeliveryApp.Application.Users.Queries.Dtos;
using DeliveryApp.Domain.Entities.User;
using Riok.Mapperly.Abstractions;

namespace DeliveryApp.Application.Common.Mappings;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class UserDtoMapperly
{
    public static partial IQueryable<UserDto> ProjectToModel(this IQueryable<User> model);
}