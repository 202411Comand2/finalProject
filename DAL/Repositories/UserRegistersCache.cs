using DAL.Abstractions;
using DAL.ConfigSettings;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DAL.Repositories
{
    public class UserRegistersCache : IRedisRepository<UserRegisterAttempt>
    {
        private readonly RedisOptions _options;
        public UserRegistersCache(IOptions<RedisOptions> options)
        {
            _options = options.Value;
        }
        public async Task Add(UserRegisterAttempt entity)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_options.ConnectionString);
            IDatabase db = redis.GetDatabase();

            await db.StringSetAsync(entity.Id.ToString(),
                JsonConvert.SerializeObject(entity),
                expiry: TimeSpan.FromHours(1));
        }

        public async Task<UserRegisterAttempt?> Get(Guid id)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_options.ConnectionString);
            IDatabase db = redis.GetDatabase();

            return JsonConvert.DeserializeObject<UserRegisterAttempt>(await db.StringGetAsync(id.ToString()));
        }
    }
}
