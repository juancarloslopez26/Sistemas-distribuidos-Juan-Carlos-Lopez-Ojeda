using SoapApi.Dtos;

namespace SoapApi.Repositories;

 public interface IUserRepository{
        Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<UserModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<IList<UserModel>> GetAllByEmailAsync(string email, CancellationToken cancellationToken);    
}