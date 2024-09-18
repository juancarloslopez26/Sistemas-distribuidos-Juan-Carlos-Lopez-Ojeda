using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using RestApi.Services;
using RestApi.Mappers;

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
    public async Task<ActionResult<GroupResponse>> GetGroupById(string Id, CancellationToken cancellationToken)
    {
        var group = await _groupService.GetGroupByIdAsync(Id, cancellationToken);
        if(group is null)
        {
            return NotFound();
        }
        return Ok(group.ToDto());
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroupsByName([FromQuery] string name, CancellationToken cancellationToken)
    {
        var groups = await _groupService.GetGroupsByNameAsync(name, cancellationToken);
        if(groups == null || !groups.Any())
        {
            return Ok(new List<GroupResponse>());
        }
        return Ok(groups.Select(group => group.ToDto()));
    }
}