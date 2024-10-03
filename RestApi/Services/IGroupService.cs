using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService{
    Task<GroupUserModel> GetGroupByIdAsync (string Id, CancellationToken cancellationToken);
    //paginacion tarea*
    Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, int pageIndex, int pageSize, string orderBy, CancellationToken cancellationToken);

    Task DeleteGroupByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken);

    Task<GroupUserModel> GetGroupByExactNameAsync(string name, CancellationToken cancellationToken);

    Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken);
}