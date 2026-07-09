using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record TaskCreatedEvent(Guid TaskId) : IDomainEvent;
