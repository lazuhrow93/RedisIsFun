using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Controller
{
    public class RedisConnector
    {
        public ConnectionMultiplexer Multiplexor;
        public IDatabase Redis;

        public RedisConnector(ConfigurationOptions options)
        {
            Multiplexor = ConnectionMultiplexer.Connect(options);
            Redis = Multiplexor.GetDatabase();
        }

        public TimeSpan Ping()
        {
            return Redis.Ping();
        }

        public Task<TimeSpan> PingAsync()
        {
            return Redis.PingAsync();
        }

        public IBatch StartBatch()
        {
            return Redis.CreateBatch();
        }

        public void SetString(RedisKey keyName, string value, TimeSpan? timeToExpire)
        {
            if (timeToExpire is null)
                Redis.StringSet(keyName, value);
            else
                Redis.StringSet(keyName, value, timeToExpire);
        }

        public string? GetString(RedisKey keyName)
        {
            return Redis.StringGet(keyName);
        }

        public TimeSpan? GetTTLForString(RedisKey keyName)
        {
            return Redis.KeyTimeToLive(keyName);
        }
    }
}
