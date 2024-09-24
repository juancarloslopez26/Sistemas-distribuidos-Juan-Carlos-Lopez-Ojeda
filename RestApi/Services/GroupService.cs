using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository){
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<GroupUserModel> CreateGroupAsync(GroupUserModel newGroup, CancellationToken cancellationToken)
{
    
    var createdGroup = new GroupUserModel
    {
        Id = Guid.NewGuid().ToString(),
        Name = newGroup.Name,
        CreationDate = DateTime.UtcNow,
        Users = newGroup.Users
    };


    return createdGroup; 
}


    public async Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(Id, cancellationToken);
        if(group is null){
            return null;
        }
        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken)))).Where(user => user !=null).ToList()

        };
    }

    public Task<GroupUserModel> GetGroupByIdAsync(GroupUserModel newGroup, string Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken) // Nuevo método
    {
        var groups = await _groupRepository.GetByNameAsync(name, cancellationToken);
        return groups.Select(group => new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
             Users = new List<UserModel>() 
        });
    }

    public Task PatchGroupAsync(string id, GroupUserModel partialUpdateGroup, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task IGroupService.CreateGroupAsync(GroupUserModel newGroup, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}