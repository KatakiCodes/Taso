using Taso.Application.Categories.DTOs;
using Taso.Application.Categories.Queries;
using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

namespace Taso.Application.Categories.Handlers;

public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
    {
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<IEnumerable<CategoryDto>>> HandleAsync(GetAllCategoriesQuery query, CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<IEnumerable<CategoryDto>>.Failure("Não autorizado.");

        var categories = await _categoryRepository.GetAllByUserIdAsync(userId, cancellationToken);

        var dtos = categories.Select(c => new CategoryDto(c.Id, c.Name, c.Color));

        return Result<IEnumerable<CategoryDto>>.Success(dtos);
    }
}
