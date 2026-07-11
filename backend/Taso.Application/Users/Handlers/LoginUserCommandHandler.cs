using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;

using Taso.Application.Users.Commands;
namespace Taso.Application.Users.Handlers;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> HandleAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        return await _identityService.LoginAsync(command.Email, command.Password);
    }
}
