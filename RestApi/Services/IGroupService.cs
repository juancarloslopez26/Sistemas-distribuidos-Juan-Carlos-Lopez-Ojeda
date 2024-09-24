using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService{

    Task<GroupUserModel> CreateGroupAsync(GroupUserModel newGroup, CancellationToken cancellationToken);  
    Task<GroupUserModel> GetGroupByIdAsync (GroupUserModel newGroup, string Id, CancellationToken cancellationToken);
    Task GetGroupByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken); // Nuevo método
    Task PatchGroupAsync(string id, GroupUserModel partialUpdateGroup, CancellationToken cancellationToken);
}