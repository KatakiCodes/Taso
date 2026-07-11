using Taso.Application.Categories.DTOs;
using Taso.Application.Categories.Queries;
using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

namespace Taso.Application.Categories.Handlers;

public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
    {
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<CategoryDto>> HandleAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(query.Id, cancellationToken);

        if (category == null)
            return Result<CategoryDto>.Failure("Categoria não encontrada.");

        if (category.UserId != _currentUserService.UserId)
            return Result<CategoryDto>.Failure("Você não tem permissão para realizar ações neste recurso.");

        var dto = new CategoryDto(category.Id, category.Name, category.Color);

        return Result<CategoryDto>.Success(dto);
    }
}
