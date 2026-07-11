using System;
using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record CategoryCreatedEvent(Guid CategoryId) : IDomainEvent;
