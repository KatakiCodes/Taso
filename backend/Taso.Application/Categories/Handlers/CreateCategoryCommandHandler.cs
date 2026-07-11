using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;

using Taso.Application.Categories.Commands;
namespace Taso.Application.Categories.Handlers;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> HandleAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var category = new Category(command.Name, command.Color);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(category.Id);
    }
}
