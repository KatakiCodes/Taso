using Taso.Application.Categories.DTOs;
using Taso.Application.Common.CQRS;

namespace Taso.Application.Categories.Queries;

public record GetAllCategoriesQuery() : IQuery<IEnumerable<CategoryDto>>;
