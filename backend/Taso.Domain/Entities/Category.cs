using Taso.Domain.Common;

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
    }

    public void Update(string name, string color)
    {
        Name = name;
        Color = color;
        UpdateTimestamp();
    }
}
