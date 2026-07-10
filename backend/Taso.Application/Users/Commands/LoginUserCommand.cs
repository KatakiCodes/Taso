using System.ComponentModel.DataAnnotations;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Users.Commands;

public record LoginUserCommand(
    [Required] [EmailAddress] string Email,
    [Required] string Password
) : ICommand<string>;
