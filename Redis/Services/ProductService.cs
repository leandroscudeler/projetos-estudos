using ApiRedisSample.Data;
using ApiRedisSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRedisSample.Services
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        private readonly RedisService _redis;

        public ProductService(AppDbContext db, RedisService redis)
        {
            _db = db;
            _redis = redis;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var cacheKey = $"product:{id}";

            // 1) Try cache
            var cached = await _redis.GetAsync<Product>(cacheKey);
            if (cached is not null)
            {
                Console.WriteLine("⚡ Cache hit (Redis)");
                return cached;
            }

            // 2) Fallback to DB
            var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return null;

            // 3) Store in cache (5 minutes TTL)
            await _redis.SetAsync(cacheKey, product, TimeSpan.FromMinutes(5));

            return product;
        }
    }
}
