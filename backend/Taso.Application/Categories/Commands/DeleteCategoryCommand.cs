using System.ComponentModel.DataAnnotations;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Categories.Commands;

public record DeleteCategoryCommand(
    [property: Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    Guid Id
) : ICommand;
