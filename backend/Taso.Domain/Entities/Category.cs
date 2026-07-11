using Taso.Domain.Common;

using Taso.Domain.Events;

namespace Taso.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Color { get; private set; } = "#FFFFFF";
    public string UserId { get; private set; } = string.Empty;

    private Category() { } // For EF Core

    public Category(string name, string color, string userId)
    {
        Name = name;
        Color = color;
        UserId = userId;
        
        AddDomainEvent(new CategoryCreatedEvent(Id));
    }

    public void Update(string name, string color)
    {
        Name = name;
        Color = color;
        UpdateTimestamp();
        
        AddDomainEvent(new CategoryUpdatedEvent(Id));
    }
    
    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdateTimestamp();
        
        AddDomainEvent(new CategoryDeletedEvent(Id));
    }
}
