using Mapster;
using Taso.Application.Common.CQRS;
using Taso.Domain.Common;
using Taso.Domain.Repositories;
using Taso.Application.TaskItems.DTOs;
using Taso.Application.TaskItems.Queries;

namespace Taso.Application.TaskItems.Handlers;

public class GetTaskItemByIdQueryHandler : IQueryHandler<GetTaskItemByIdQuery, TaskItemDto>
{
    private readonly ITaskItemRepository _taskItemRepository;

    public GetTaskItemByIdQueryHandler(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<Result<TaskItemDto>> HandleAsync(GetTaskItemByIdQuery query, CancellationToken cancellationToken = default)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(query.TaskId, cancellationToken);

        if (taskItem == null)
        {
            return Result<TaskItemDto>.Failure("Task not found");
        }

        var dto = taskItem.Adapt<TaskItemDto>();

        return Result<TaskItemDto>.Success(dto);
    }
}
