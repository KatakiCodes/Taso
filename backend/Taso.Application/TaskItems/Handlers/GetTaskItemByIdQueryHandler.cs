using Mapster;
using Taso.Application.Common.CQRS;
using Taso.Application.Common.Interfaces;
using Taso.Application.TaskItems.DTOs;
using Taso.Application.TaskItems.Queries;
using Taso.Domain.Common;
using Taso.Domain.Repositories;

namespace Taso.Application.TaskItems.Handlers;

public class GetTaskItemByIdQueryHandler : IQueryHandler<GetTaskItemByIdQuery, TaskItemDto>
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository, ICurrentUserService currentUserService)
    {
        _taskItemRepository = taskItemRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<TaskItemDto>> HandleAsync(GetTaskItemByIdQuery query, CancellationToken cancellationToken = default)
    {
        var task = await _taskItemRepository.GetByIdAsync(query.TaskId, cancellationToken);

        if (task == null)
        {
            return Result<TaskItemDto>.Failure("Tarefa não encontrada.");
        }

        if (task.UserId != _currentUserService.UserId)
        {
            return Result<TaskItemDto>.Failure("Você não tem permissão para realizar ações neste recurso.");
        }

        var dto = task.Adapt<TaskItemDto>();

        return Result<TaskItemDto>.Success(dto);
    }
}
