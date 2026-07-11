using Taso.Application.Common.CQRS;
using Taso.Application.TaskItems.DTOs;

namespace Taso.Application.TaskItems.Queries;

public record GetTaskItemByIdQuery(Guid TaskId) : IQuery<TaskItemDto>;
