using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Commands;
namespace Taso.Application.Categories.Handlers;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<Guid>.Failure("Não autorizado.");

        var category = new Category(command.Name, command.Color, userId);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(category.Id);
    }
}
