namespace ToDoManagement.Common;

public class RedisOptions
{
    public const string RedisConfiguration = nameof(RedisConfiguration);
    public string ConnectionString { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
}
