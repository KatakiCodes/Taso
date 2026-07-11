using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Domain.Common;
using Taso.Domain.Enums;
using Taso.Domain.Repositories;
using Taso.Application.TaskItems.Commands;

namespace Taso.Application.TaskItems.Handlers;

public class CompleteTaskItemCommandHandler : ICommandHandler<CompleteTaskItemCommand>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CompleteTaskItemCommandHandler(ITaskItemRepository taskItemRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _taskItemRepository = taskItemRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result> HandleAsync(CompleteTaskItemCommand command, CancellationToken cancellationToken = default)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(command.TaskId, cancellationToken);
        
        if (taskItem == null)
        {
            return Result.Failure("Tarefa não encontrada.");
        }

        if (taskItem.UserId != _currentUserService.UserId)
        {
            return Result.Failure("Você não tem permissão para realizar ações neste recurso.");
        }

        taskItem.ChangeState(TaskState.Completed);
        
        _taskItemRepository.Update(taskItem);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
