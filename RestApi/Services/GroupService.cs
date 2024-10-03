using RestApi.Exceptions;
using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task DeleteGroupByIdAsync(string id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);

            throw new GroupNotFoundException();
        }

        await _groupRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(Id, cancellationToken);
        if (group is null)
        {
            return null;
        }

        return new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken))))
                        .Where(user => user != null).ToList()
        };
    }


    public async Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, int pageIndex, int pageSize, string orderBy, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByNameAsync(name, cancellationToken);


        {
            var users = await Task.WhenAll(group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken)));
            return new GroupUserModel
            {
                Id = group.Id,
                Name = group.Name,
                CreationDate = group.CreationDate,
                Users = users.Where(user => user != null).ToList()
            };
        }));

        var orderedGroups = orderBy switch
        {
            "name" => groupUserModels.OrderBy(g => g.Name),
            "creationDate" => groupUserModels.OrderBy(g => g.CreationDate),
            _ => groupUserModels.OrderBy(g => g.Name)
        };

        return orderedGroups
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken)
    {

        if (users.Length == 0)
        {
            throw new InvalidGroupRequestFormatException();
        }

        // Validar duplicados mediante búsqueda exacta
        var existingGroup = await _groupRepository.GetByExactNameAsync(name, cancellationToken);
        if (existingGroup != null)
        {
            throw new GroupAlreadyExistsExceptions();
        }

        var group = await _groupRepository.CreateAsync(name, users, cancellationToken);
        return new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken))))
                        .Where(user => user != null).ToList()

        };
    }
}
