using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Commands;
namespace Taso.Application.Categories.Handlers;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(UpdateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(command.Id, cancellationToken);

        if (category == null)
            return Result.Failure("Categoria não encontrada.");

        category.Update(command.Name, command.Color);

        _categoryRepository.Update(category);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
