namespace TaskShare.Core.DomainEntities;

public class TaskList
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int OwnerId { get; set; }

    public List<int> SharedWithUserIds { get; set; } = [];
}