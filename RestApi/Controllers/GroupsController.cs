using Microsoft.AspNetCore.Mvc;
using RestApi.Services;
using RestApi.Mappers;
using RestApi.Models;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupUserModel>> GetGroupById(string id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group is null)
        {
            return NotFound();
        }

        return Ok(group);
    }

    // Endpoint para obtener grupos por nombre, con paginación y ordenamiento
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupUserModel>>> GetGroupsByName(
        [FromQuery] string name, 
        [FromQuery] int pageIndex, 
        [FromQuery] int pageSize,
        [FromQuery] string orderBy , // Parámetro para ordenamiento
        CancellationToken cancellationToken)
    {
        var groups = await _groupService.GetGroupsByNameAsync(name, cancellationToken);

        // Ordenar los grupos según el parámetro 'orderBy'
        var orderedGroups = orderBy switch
        {
            "Name" => groups.OrderBy(g => g.Name),
            "CreationDate" => groups.OrderBy(g => g.CreationDate),
            _ => groups.OrderBy(g => g.CreationDate) // Orden predeterminado
        };

        // Aplicar paginación
        var pagedGroups = orderedGroups
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();

        // Devolver la lista de grupos paginados y ordenados
        return Ok(pagedGroups);
    }

    // DELETE localhost/groups/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(string id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (group is null)
        {
            return NotFound();
        }

        // Lógica para eliminar el grupo, si aplica, en el servicio.
        // Ejemplo: await _groupService.DeleteGroupByIdAsync(id, cancellationToken);

        return NoContent();
    }

    // POST localhost/groups
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] GroupUserModel newGroup, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdGroup = await _groupService.CreateGroupAsync(newGroup, cancellationToken);

        if (createdGroup is null)
        {
            return Conflict("A group with the same name already exists.");
        }

        return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
    }

    // PUT localhost/groups/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(string id, [FromBody] GroupUserModel updatedGroup, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingGroup = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (existingGroup is null)
        {
            return NotFound();
        }

        var result = await _groupService.UpdateGroupAsync(id, updatedGroup, cancellationToken);
        if (result is null)
        {
            return Conflict("A conflict occurred while updating the group.");
        }

        return Ok(result);
    }

    // PATCH localhost/groups/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchGroup(string id, [FromBody] GroupUserModel partialUpdateGroup, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingGroup = await _groupService.GetGroupByIdAsync(id, cancellationToken);
        if (existingGroup is null)
        {
            return NotFound();
        }

        var result = await _groupService.PatchGroupAsync(id, partialUpdateGroup, cancellationToken);
        if (result is null)
        {
            return Conflict("A conflict occurred while patching the group.");
        }

        return Ok(result);
    }
}
