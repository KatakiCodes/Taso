using Microsoft.AspNetCore.Mvc;
using Taso.Application.Common.CQRS;
using Taso.Application.Users.Commands;
using Taso.Application.Users.DTOs;

namespace Taso.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="command">Dados do usuário para registro (Nome, Email, Senha).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Objeto contendo o ID do usuário criado.</returns>
    /// <response code="200">Usuário registrado com sucesso.</response>
    /// <response code="400">Dados inválidos ou usuário já existente.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResponseDto), 200)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    public async Task<IActionResult> Register(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(new RegisterResponseDto(result.Value!));
    }

    /// <summary>
    /// Realiza login do usuário e retorna um token JWT.
    /// </summary>
    /// <param name="command">Credenciais do usuário (Email, Senha).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Objeto contendo o token JWT.</returns>
    /// <response code="200">Login realizado com sucesso e token gerado.</response>
    /// <response code="400">Credenciais inválidas.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(typeof(Taso.Domain.Common.Result), 400)]
    public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.SendAsync(command, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(new AuthResponseDto(result.Value!));
    }
}
