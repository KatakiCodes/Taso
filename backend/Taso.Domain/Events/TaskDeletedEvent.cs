using System;
using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record TaskDeletedEvent(Guid TaskId) : IDomainEvent;
