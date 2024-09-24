using RestApi.Models;

namespace RestApi.Repositories;

public interface IUserRepository{
    Task<UserModel> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserModel> GetByIdAsync(UserModel userId, CancellationToken cancellationToken);
}