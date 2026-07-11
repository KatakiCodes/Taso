using System;
using Taso.Domain.Common;

namespace Taso.Domain.Events;

public record CategoryDeletedEvent(Guid CategoryId) : IDomainEvent;
