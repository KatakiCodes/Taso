using Taso.Application.Categories.DTOs;
using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Queries;
namespace Taso.Application.Categories.Handlers;

public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryDto>> HandleAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(query.Id, cancellationToken);

        if (category == null)
            return Result<CategoryDto>.Failure("Categoria não encontrada.");

        var dto = new CategoryDto(category.Id, category.Name, category.Color);

        return Result<CategoryDto>.Success(dto);
    }
}
