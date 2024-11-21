using RestApi.Models;
using RestApi.Infrastructure.Soap;

namespace RestApi.Mappers;

public static class UserMapper{
    public static UserModel ToDomain(this UserResponseDto user){
        if (user is null){
            return null;
        }
        return new UserModel{
            Id =user.UserId,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Email = user.Email,
            Birthday= user.BirthDate
        };
    }
}