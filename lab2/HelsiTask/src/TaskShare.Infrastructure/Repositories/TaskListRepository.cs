using MongoDB.Driver;
using TaskShare.Core.DomainEntities;
using TaskShare.Core.Repositories;
using TaskShare.Infrastructure.Data;
using TaskShare.Infrastructure.Mapping;

namespace TaskShare.Infrastructure.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly TaskShareDbContext _dbContext;
    public TaskListRepository(TaskShareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskList>> GetPagedAsync(int page, int pageSize, int userId)
    {
        var mongoEntities = await _dbContext.TaskLists
            .Find(taskList => taskList.OwnerId == userId || taskList.SharedWithUserIds.Contains(userId))
            .SortByDescending(taskList => taskList.CreationDate)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return mongoEntities.Select(x => x.ToDomain());
    }
    public async Task<IEnumerable<TaskList>> GetAllAsync()
    {
        var mongoEntities = await _dbContext.TaskLists
            .Find(taskList => true)
            .ToListAsync();

        return mongoEntities.Select(x => x.ToDomain());
    }

    public async Task<TaskList?> GetByIdAsync(string id)
    {
        var mongoEntity = await _dbContext.TaskLists
            .Find(taskList => taskList.Id == id)
            .FirstOrDefaultAsync();

        return mongoEntity?.ToDomain();
    }

    public async Task CreateAsync(TaskList taskList)
    {
        var mongoEntity = taskList.ToMongo();
        await _dbContext.TaskLists.InsertOneAsync(mongoEntity);
    }

    public async Task UpdateAsync(string id, TaskList updatedTaskList)
    {
        var mongoEntity = updatedTaskList.ToMongo();
        await _dbContext.TaskLists.ReplaceOneAsync(
            taskList => taskList.Id == id,
            mongoEntity
        );
    }

    public async Task DeleteAsync(string id)
    {
        await _dbContext.TaskLists.DeleteOneAsync(taskList => taskList.Id == id);
    }

}