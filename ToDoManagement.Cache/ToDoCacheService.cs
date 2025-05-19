using StackExchange.Redis;
using System.Text.Json;

namespace ToDoManagement.Cache;

public class ToDoCacheService : IToDoCacheService
{
    private Lazy<IConnectionMultiplexer> _connection;

    public ToDoCacheService(string connectionString) => _connection = new(() =>
        {
            return ConnectionMultiplexer.Connect(connectionString);
        });

    public IConnectionMultiplexer Connection
    {
        get
        {
            return _connection.Value;
        }
    }

    public IDatabase Database { get => Connection.GetDatabase(); }

    public async Task<T?> GetValueByKey<T>(
        string key,
        CommandFlags commandFlags = CommandFlags.None) where T : class
    {
        RedisValue? redisValue = await Database.StringGetAsync(key, commandFlags);

        if (redisValue is null || redisValue.HasValue is false)
        {
            return null;
        }

        return JsonSerializer.Deserialize<T>(json: redisValue!);
    }

    public async Task SetKey<T>(
        string key,
        T value,
        TimeSpan? expireTimespan = null,
        bool keepTimeToLive = false,
        When whenToSetTheValue = When.Always,
        CommandFlags commandFlags = CommandFlags.None)
    {
        var redisValue = new RedisValue(JsonSerializer.Serialize(value));

        await Database.StringSetAsync(key, redisValue, expireTimespan, keepTimeToLive,
            whenToSetTheValue, commandFlags);
    }
}
