using System.ComponentModel.DataAnnotations;
using Taso.Application.Categories.DTOs;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Categories.Queries;

public record GetCategoryByIdQuery(
    [property: Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    Guid Id
) : IQuery<CategoryDto>;
