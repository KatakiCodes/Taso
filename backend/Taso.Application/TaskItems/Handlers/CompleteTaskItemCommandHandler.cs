using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Enums;
using Taso.Domain.Repositories;
using Taso.Application.TaskItems.Commands;

namespace Taso.Application.TaskItems.Handlers;

public class CompleteTaskItemCommandHandler : ICommandHandler<CompleteTaskItemCommand>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork)
    {
        _taskItemRepository = taskItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(CompleteTaskItemCommand command, CancellationToken cancellationToken = default)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(command.TaskId, cancellationToken);
        
        if (taskItem == null)
        {
            return Result.Failure("Task not found");
        }

        taskItem.ChangeState(TaskState.Completed);
        
        _taskItemRepository.Update(taskItem);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
