using Taso.Application.Common.CQRS;

namespace Taso.Application.TaskItems.Commands;

public record CompleteTaskItemCommand(Guid TaskId) : ICommand;
