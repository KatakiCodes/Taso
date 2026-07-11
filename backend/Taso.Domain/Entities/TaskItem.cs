using Taso.Domain.Common;
using Taso.Domain.Enums;
using Taso.Domain.Events;

namespace Taso.Domain.Entities;

public class TaskItem : BaseEntity
{
    private readonly List<TaskItem> _subTasks = new();

    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime? DueDate { get; private set; }
    public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
    public TaskState State { get; private set; } = TaskState.Todo;

    public string UserId { get; private set; } = string.Empty;

    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public Guid? ParentTaskId { get; private set; }
    public TaskItem? ParentTask { get; private set; }

    public IReadOnlyCollection<TaskItem> SubTasks => _subTasks.AsReadOnly();

    private TaskItem() { } // For EF Core

    public TaskItem(string title, string description, DateTime? dueDate, TaskPriority priority, Guid? categoryId, Guid? parentTaskId, string userId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        CategoryId = categoryId;
        ParentTaskId = parentTaskId;
        UserId = userId;
        State = TaskState.Todo;

        AddDomainEvent(new TaskCreatedEvent(Id));
    }

    public void UpdateDetails(string title, string description, DateTime? dueDate, TaskPriority priority, Guid? categoryId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        CategoryId = categoryId;
        UpdateTimestamp();
        
        AddDomainEvent(new TaskUpdatedEvent(Id));
    }

    public void AddSubTask(TaskItem subTask)
    {
        _subTasks.Add(subTask);
        UpdateTimestamp();
    }

    public void ChangeState(TaskState newState)
    {
        if (State == newState) return;

        State = newState;
        UpdateTimestamp();

        if (State == TaskState.Completed)
        {
            AddDomainEvent(new TaskCompletedEvent(Id));
        }
        else 
        {
            AddDomainEvent(new TaskUpdatedEvent(Id));
        }
    }
    
    public void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdateTimestamp();
        
        AddDomainEvent(new TaskDeletedEvent(Id));
    }
}
