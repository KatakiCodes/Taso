using Microsoft.AspNetCore.Mvc;
using Taso.Application.Categories.Commands;
using Taso.Application.Categories.DTOs;
using Taso.Application.Categories.Queries;
using Taso.Application.Common.CQRS;

namespace Taso.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Lista todas as categorias.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Lista de categorias (CategoryDto).</returns>
    /// <response code="200">Categorias retornadas com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await _sender.SendAsync(query, cancellationToken);
        
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtém os detalhes de uma Categoria específica.
    /// </summary>
    /// <param name="id">O ID da Categoria desejada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Os dados da Categoria (CategoryDto).</returns>
    /// <response code="200">Categoria encontrada e retornada.</response>
    /// <response code="404">A categoria com o ID fornecido não foi encontrada.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _sender.SendAsync(query, cancellationToken);
        
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result.Value);
    }

    /// <summary>
    /// Cria uma nova Categoria.
    /// </summary>
    /// <param name="command">Dados necessários para criar a categoria (Nome e Cor).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>O identificador único (Guid) da Categoria recém criada.</returns>
    /// <response code="201">Categoria criada com sucesso.</response>
    /// <response code="400">Os dados informados são inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Atualiza uma Categoria existente.
    /// </summary>
    /// <param name="id">ID da categoria a ser atualizada.</param>
    /// <param name="command">Novos dados da categoria.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Sem conteúdo em caso de sucesso.</returns>
    /// <response code="204">Categoria atualizada com sucesso.</response>
    /// <response code="400">A requisição é inválida (IDs não coincidem ou erro de validação).</response>
    /// <response code="404">A categoria com o ID fornecido não foi encontrada.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 404)]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(Taso.Domain.Common.Result.Failure("O ID da rota não coincide com o ID do payload."));

        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }

    /// <summary>
    /// Exclui uma Categoria.
    /// </summary>
    /// <param name="id">O ID da categoria que será excluída.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Sem conteúdo em caso de sucesso.</returns>
    /// <response code="204">Categoria excluída com sucesso.</response>
    /// <response code="404">A categoria não foi encontrada.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 404)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _sender.SendAsync(command, cancellationToken);
        
        if (!result.IsSuccess)
            return NotFound(result);

        return NoContent();
    }
}
