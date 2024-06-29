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
    }
}
