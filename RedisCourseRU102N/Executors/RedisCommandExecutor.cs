using RedisCourseRU102N.ConnectingAndPing;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Controller
{
    public class RedisCommandExecutor : IRedisCommandExecutor
    {
        private RedisConnector _redisExecutor { get; set; }
        private IBatch? _commandBatch;

        public RedisCommandExecutor(ConfigurationOptions options)
        {
            _redisExecutor = new(options);
        }

        public TimeSpan Ping()
        {
            return _redisExecutor.Ping();
        }

        public Task<TimeSpan> PingAsync()
        {
            return _redisExecutor.PingAsync();
        }

        public Task<TimeSpan> PingAsyncOnBatch()
        {
            if (_commandBatch is null) throw new Exception($"Initialize the batch first");
            return _commandBatch.PingAsync();
        }

        public void StartBatch()
        {
            if (_commandBatch != null) return;
            _commandBatch = _redisExecutor.StartBatch();
        }

        public void ExecuteBatch()
        {
            if (_commandBatch is null) throw new Exception($"Initialize the batch first");
            _commandBatch.Execute();
        }

        public void SetString(RedisKey keyName, string value)
        {
            _redisExecutor.SetString(keyName, value, null);
        }

        public void SetString(RedisKey keyName, string value, TimeSpan timeToExpire)
        {
            _redisExecutor.SetString(keyName, value, timeToExpire);
        }

        public string? GetString(RedisKey keyName)
        {
            return _redisExecutor.GetString(keyName);
        }

        public TimeSpan? GetTTL(RedisKey keyName)
        {
            return _redisExecutor.GetTTLForString(keyName);
        }

        public void ClearKey(params string[] keysToDelete)
        {
            _redisExecutor.DeleteKeys(keysToDelete.Select(k => new RedisKey(k)).ToArray());
        }

        public void PushLeftList(string key, IEnumerable<string> vals)
        {
            var redisVals = vals.Select(s => new RedisValue(s));
            _redisExecutor.PushLeftList(new RedisKey(key), redisVals.ToArray());
        }

        public void PushRightList(string key, IEnumerable<string> vals)
        {
            var redisVals = vals.Select(s => new RedisValue(s));
            _redisExecutor.PushRightList(new RedisKey(key), redisVals.ToArray());
        }

        public string? GetFromList(string key, int index)
        {
            return _redisExecutor.GetFromList(key, index);
        }

        public IEnumerable<string>? GetRange(string key, int start, int end)
        {
            return _redisExecutor.GetRange(new RedisKey(key), start, end);
        }
    }
}
