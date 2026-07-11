using System.ComponentModel.DataAnnotations;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Categories.Commands;

public record CreateCategoryCommand(
    [property: Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [property: MaxLength(100, ErrorMessage = "O nome da categoria não pode exceder 100 caracteres.")]
    string Name,

    [property: Required(ErrorMessage = "A cor da categoria é obrigatória.")]
    [property: RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "A cor deve ser um código HEX válido (ex: #FFFFFF).")]
    string Color
) : ICommand<Guid>;
