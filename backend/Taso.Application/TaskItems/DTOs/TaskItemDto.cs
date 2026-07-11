using Taso.Domain.Enums;

namespace Taso.Application.TaskItems.DTOs;

public record TaskItemDto(
    Guid Id,
    string Title,
    string Description,
    DateTime? DueDate,
    TaskPriority Priority,
    TaskState State,
    Guid? CategoryId,
    Guid? ParentTaskId);
