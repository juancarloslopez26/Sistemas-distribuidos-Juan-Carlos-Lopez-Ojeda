using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository
{
    Task<GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<GroupModel>> GetByNameAsync(string name, CancellationToken cancellationToken); 
    Task<List<GroupModel>> GetGroupsByNameAsync(
        string name, 
        int pageIndex, 
        int pageSize, 
        string orderBy, 
        CancellationToken cancellationToken);

        Task DeleteByIdAsync (string id, CancellationToken cancellationToken);
        Task <GroupModel> CreateAsync(string name, Guid[] users, CancellationToken cancellationToken);
        Task<GroupModel> GetGroupByExactNameAsync(string name, CancellationToken cancellationToken);

        Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken);

}
