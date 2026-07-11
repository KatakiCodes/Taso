using Taso.Application.Categories.DTOs;
using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Queries;
namespace Taso.Application.Categories.Handlers;

public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<CategoryDto>>> HandleAsync(GetAllCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        var dtos = categories.Select(c => new CategoryDto(c.Id, c.Name, c.Color));

        return Result<IEnumerable<CategoryDto>>.Success(dtos);
    }
}
