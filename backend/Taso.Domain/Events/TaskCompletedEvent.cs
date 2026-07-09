using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record TaskCompletedEvent(Guid TaskId) : IDomainEvent;
