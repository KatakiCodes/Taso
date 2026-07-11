using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;

using Taso.Application.Users.Commands;
namespace Taso.Application.Users.Handlers;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<string>> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        return await _identityService.RegisterUserAsync(command.Email, command.Password, command.FullName);
    }
}
