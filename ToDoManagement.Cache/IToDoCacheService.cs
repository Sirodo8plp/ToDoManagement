using StackExchange.Redis;

namespace ToDoManagement.Cache;

public interface IToDoCacheService
{
    public IConnectionMultiplexer Connection { get; }
    public IDatabase Database { get; }
    public Task<T?> GetValueByKey<T>(
        string key,
        CommandFlags commandFlags = CommandFlags.None) where T : class;
    public Task SetKey<T>(
        string key,
        T value,
        TimeSpan? expireTimespan = null,
        bool keepTimeToLive = false,
        When whenToSetTheValue = When.Always,
        CommandFlags commandFlags = CommandFlags.None);
}
