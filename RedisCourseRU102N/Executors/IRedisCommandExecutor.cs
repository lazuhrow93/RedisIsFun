﻿using StackExchange.Redis;

namespace RedisCourseRU102N.Controller
{
    public interface IRedisCommandExecutor
    {
        public TimeSpan Ping();
        public Task<TimeSpan> PingAsync();
        public Task<TimeSpan> PingAsyncOnBatch();
        void StartBatch();
        void ExecuteBatch();
        void SetString(RedisKey keyName, string value);
        void SetString(RedisKey keyName, string value, TimeSpan timeToExpire);
        string? GetString(RedisKey keyName);
        TimeSpan? GetTTL(RedisKey keyName);
        void ClearKey(params string[] keysToDelete);
        void PushLeftList(string key, IEnumerable<string> vals);
        string? GetFromList(string key, int index);
    }
}
