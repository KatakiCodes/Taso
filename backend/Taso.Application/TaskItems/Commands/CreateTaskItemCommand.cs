using Taso.Application.Common.CQRS;
using Taso.Domain.Enums;

namespace Taso.Application.TaskItems.Commands;

public record CreateTaskItemCommand(
    string Title,
    string Description,
    DateTime? DueDate,
    TaskPriority Priority,
    Guid? CategoryId,
    Guid? ParentTaskId) : ICommand<Guid>;
