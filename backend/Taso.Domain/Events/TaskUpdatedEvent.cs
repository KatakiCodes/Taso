using System;
using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record TaskUpdatedEvent(Guid TaskId) : IDomainEvent;
