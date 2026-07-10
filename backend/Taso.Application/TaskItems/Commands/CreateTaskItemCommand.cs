using System.ComponentModel.DataAnnotations;
using Taso.Application.Common.CQRS;
using Taso.Domain.Enums;

namespace Taso.Application.TaskItems.Commands;

public record CreateTaskItemCommand(
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title must not exceed 200 characters")]
    string Title,
    string Description,
    DateTime? DueDate,
    TaskPriority Priority,
    Guid? CategoryId,
    Guid? ParentTaskId) : ICommand<Guid>;
