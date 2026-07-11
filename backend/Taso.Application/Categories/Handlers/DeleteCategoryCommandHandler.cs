using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Commands;
namespace Taso.Application.Categories.Handlers;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(DeleteCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(command.Id, cancellationToken);

        if (category == null)
            return Result.Failure("Categoria não encontrada.");

        _categoryRepository.Delete(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
