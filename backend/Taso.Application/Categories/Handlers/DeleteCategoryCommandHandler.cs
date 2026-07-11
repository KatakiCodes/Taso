using Taso.Application.Categories.Commands;
using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

namespace Taso.Application.Categories.Handlers;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result> HandleAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(command.Id, cancellationToken);

        if (category == null)
            return Result.Failure("Categoria não encontrada.");

        if (category.UserId != _currentUserService.UserId)
            return Result.Failure("Você não tem permissão para realizar ações neste recurso.");

        _categoryRepository.Delete(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
