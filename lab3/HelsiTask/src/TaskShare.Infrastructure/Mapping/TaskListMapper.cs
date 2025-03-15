using TaskShare.Infrastructure.Entities.MongoEntities;

namespace TaskShare.Infrastructure.Mapping;

public static class TaskListMapper
{
    public static Core.DomainEntities.TaskList ToDomain(this TaskList entity)
    {
        return new Core.DomainEntities.TaskList
        {
            Id = entity.Id,
            Name = entity.Name,
            OwnerId = entity.OwnerId,
            SharedWithUserIds = entity.SharedWithUserIds
        };
    }

    public static TaskList ToMongo(this Core.DomainEntities.TaskList domain)
    {
        return new TaskList
        {
            Id = domain.Id,
            Name = domain.Name,
            OwnerId = domain.OwnerId,
            SharedWithUserIds = domain.SharedWithUserIds,
            CreationDate = DateTime.UtcNow
        };
    }
}