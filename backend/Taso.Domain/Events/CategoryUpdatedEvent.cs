using System;
using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record CategoryUpdatedEvent(Guid CategoryId) : IDomainEvent;
