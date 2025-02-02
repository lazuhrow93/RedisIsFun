﻿using StackExchange.Redis;

namespace Providers.Interfaces
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
        void PushRightList(string key, IEnumerable<string> vals);
        string? GetFromList(string key, int index);
        IEnumerable<string>? GetRange(string key, int start, int end);
        void AddSet(string key, IEnumerable<string> val);
        IEnumerable<string>? GetSet(string key);
        void UnionAndStore(string destination, params string[] sourceKeys);
        void AddHash(string person1, Dictionary<string, object> person1Vals);
        IEnumerable<string> GetAllFieldsOfHash(string person1);
        void PrepareScript(string basicScript);
        string? EvaluateScript(object values);
        void CreateTransaction();
        Task TransactionAddHash(string key, Dictionary<string, object> vals);
        Task<bool> TransactionAddSortedSet(string key, string val, int score);
        bool ExecuteTransaction();

    }
}
