using Microsoft.AspNetCore.Mvc;
using Taso.Application.Common.CQRS;
using Taso.Application.TaskItems.Commands;
using Taso.Application.TaskItems.Queries;

namespace Taso.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly ISender _sender;

    public TaskItemsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        var command = new CompleteTaskItemCommand(id);
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTaskItemByIdQuery(id);
        var result = await _sender.SendAsync(query, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Value);
    }
}
