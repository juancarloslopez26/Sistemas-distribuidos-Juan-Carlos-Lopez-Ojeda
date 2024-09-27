using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository{
    Task<GroupModel> GetByIdAsync(string Id, CancellationToken cancellationToken);
    Task<IEnumerable<GroupModel>> GetByNameAsync(string name, int pageIndex, int pageSize, string orderBy, CancellationToken cancellationToken); // Nuevo método

    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken);

    Task<GroupModel> CreateAsync(string name, Guid[] users, CancellationToken cancellationToken);
          
    Task<GroupModel> GetByExactNameAsync(string name, CancellationToken cancellationToken);
    Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken);
}