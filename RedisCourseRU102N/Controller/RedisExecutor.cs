using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Controller
{
    public class RedisExecutor
    {
        public ConnectionMultiplexer Multiplexor;
        public IDatabase Redis;

        public RedisExecutor(ConfigurationOptions options)
        {
            Multiplexor = ConnectionMultiplexer.Connect(options);
            Redis = Multiplexor.GetDatabase();
        }

        public TimeSpan Ping()
        {
            return Redis.Ping();
        }
    }
}
