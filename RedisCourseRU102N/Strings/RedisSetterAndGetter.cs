using RedisCourseRU102N.Controller;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Strings
{
    public class RedisSetterAndGetter
    {
        public IRedisCommandExecutor _executor;

        public RedisSetterAndGetter(IRedisCommandExecutor redisCommandExecutor)
        {
            _executor = redisCommandExecutor;    
        }

        public void BasicSetAndGetString()
        {
            var key = $"laz:first:Set";
            var homeMadeKey = new RedisKey(key);
            var randomStr = DateTime.UtcNow;

            _executor.SetString(homeMadeKey, randomStr.ToString());

            var result = _executor.GetString(homeMadeKey);
            Console.WriteLine($"Here is the key value that you set for '{homeMadeKey}' result: {result} ||");
        }

        public void BasicSetAndGetWithTTL()
        {
            var key = $"laz:first:Set";
            var homeMadeKey = new RedisKey(key);
            var randomStr = DateTime.UtcNow;

            _executor.SetString(homeMadeKey, randomStr.ToString(), new TimeSpan(0, 1, 0));

            var result = _executor.GetString(homeMadeKey);
            var timeToLive = _executor.GetTTL(homeMadeKey);

            Console.WriteLine($"KeyValue'{homeMadeKey}' result: {result} || Timetolive: {timeToLive}");

        }
    }
}
