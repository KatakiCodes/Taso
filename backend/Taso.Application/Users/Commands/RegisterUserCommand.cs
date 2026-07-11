using System.ComponentModel.DataAnnotations;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Users.Commands;

public record RegisterUserCommand(
    [Required] [EmailAddress] string Email,
    [Required] [MinLength(6)] string Password,
    [Required] string FullName
) : ICommand<string>;
