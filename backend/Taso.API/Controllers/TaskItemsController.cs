using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taso.Application.Common.CQRS;
using Taso.Application.TaskItems.Commands;
using Taso.Application.TaskItems.Queries;
using Taso.Application.TaskItems.DTOs;

namespace Taso.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly ISender _sender;

    public TaskItemsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Cria uma nova Tarefa (TaskItem).
    /// </summary>
    /// <param name="command">Dados necessários para criar a tarefa (ex: Título, Prioridade, etc).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O identificador único (Guid) da Tarefa recém criada.</returns>
    /// <response code="201">Tarefa criada com sucesso.</response>
    /// <response code="400">Os dados informados são inválidos (falha na validação do Payload).</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 401)]
    public async Task<IActionResult> Create(CreateTaskItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Marca uma Tarefa como concluída.
    /// </summary>
    /// <param name="id">O ID da tarefa que será concluída.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Sem conteúdo em caso de sucesso.</returns>
    /// <response code="204">Tarefa concluída com sucesso.</response>
    /// <response code="400">Falha de negócio (ex: Tarefa já concluída, Tarefa não existe ou Acesso negado).</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpPut("{id:guid}/complete")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 401)]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        var command = new CompleteTaskItemCommand(id);
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    /// <summary>
    /// Obtém os detalhes de uma Tarefa específica.
    /// </summary>
    /// <param name="id">O ID da Tarefa desejada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Os dados da Tarefa (TaskItemDto).</returns>
    /// <response code="200">Tarefa encontrada e retornada.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="404">A tarefa não foi encontrada ou não pertence ao usuário logado (Acesso negado).</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskItemDto), 200)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 401)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 404)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTaskItemByIdQuery(id);
        var result = await _sender.SendAsync(query, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Value);
    }
}
