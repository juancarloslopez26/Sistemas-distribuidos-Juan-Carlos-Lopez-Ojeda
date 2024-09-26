using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService
{
    Task<GroupUserModel> GetGroupByIdAsync(string Id, CancellationToken cancellationToken);

    // Método para obtener grupos por nombre con paginación y ordenación
    Task<IEnumerable<GroupUserModel>> GetGroupsByNameAsync(string name, int pageIndex, int pageSize, string orderBy, CancellationToken cancellationToken);

    // Método para eliminar un grupo por ID
    Task DeleteGroupByIdAsync(string id, CancellationToken cancellationToken);

    // Método para crear un nuevo grupo
    Task<GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken);
}
