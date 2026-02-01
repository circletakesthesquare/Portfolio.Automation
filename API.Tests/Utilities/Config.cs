public static class Config
{
    private static readonly JsonElement _root;
    private static readonly JsonElement _envConfig;

    static Config()
    {
        var json = File.ReadAllText("Utilities/Environment.json");
        _root = JsonSerializer.Deserialize<JsonElement>(json);

        var current = _root.GetProperty("Current").GetString()
            ?? throw new InvalidOperationException("Current environment must be set in Environment.json");

        if (!_root.GetProperty("Environments").TryGetProperty(current, out _envConfig))
        {
            throw new InvalidOperationException($"Environment '{current}' not found in Environment.json");
        }
    }

    public static string BaseUrl => _envConfig.GetProperty("BaseUrl").GetString()
        ?? throw new InvalidOperationException("BaseUrl must be set in Environment.json");
    public static int TimeoutSeconds => _envConfig.GetProperty("TimeoutSeconds").GetInt32();
    public static string LogLevel => _envConfig.GetProperty("LogLevel").GetString()
        ?? throw new InvalidOperationException("LogLevel must be set in Environment.json");
}