using StackExchange.Redis;
using System.Text.Json;

namespace ApiRedisSample.Services
{
    public class RedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConfiguration config)
        {
            // Example: "localhost:6379" or full string "myredis:6379,abortConnect=false"
            var connString = config.GetConnectionString("Redis") ?? "localhost:6379";
            var mux = ConnectionMultiplexer.Connect(connString);
            _db = mux.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value!);
        }
    }
}
