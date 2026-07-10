using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Entities;
using Taso.Domain.Repositories;
using Taso.Application.TaskItems.Commands;

namespace Taso.Application.TaskItems.Handlers;

public class CreateTaskItemCommandHandler : ICommandHandler<CreateTaskItemCommand, Guid>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork)
    {
        _taskItemRepository = taskItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> HandleAsync(CreateTaskItemCommand command, CancellationToken cancellationToken = default)
    {
        var taskItem = new TaskItem(
            command.Title,
            command.Description,
            command.DueDate,
            command.Priority,
            command.CategoryId,
            command.ParentTaskId
        );

        await _taskItemRepository.AddAsync(taskItem, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(taskItem.Id);
    }
}
