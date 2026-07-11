using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Application.TaskItems.Commands;

namespace Taso.Application.TaskItems.Handlers;

public class CreateTaskItemCommandHandler : ICommandHandler<CreateTaskItemCommand, Guid>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _taskItemRepository = taskItemRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> HandleAsync(CreateTaskItemCommand command, CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<Guid>.Failure("Não autorizado.");

        var taskItem = new TaskItem(
            command.Title,
            command.Description,
            command.DueDate,
            command.Priority,
            command.CategoryId,
            command.ParentTaskId,
            userId
        );

        await _taskItemRepository.AddAsync(taskItem, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(taskItem.Id);
    }
}
