using Providers.Interfaces;
using StackExchange.Redis;

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

        public void AddSet(string key, IEnumerable<string> vals)
        {
            var rKey = new RedisKey(key);
            var rVals = vals.Select(s=> new RedisValue(s));
            _redisExecutor.AddSet(rKey, rVals.ToArray());
        }

        public IEnumerable<string>? GetSet(string key)
        {
            return _redisExecutor.GetSet(new RedisKey(key));
        }

        public void UnionAndStore(string destination, params string[] sourceKeys)
        {
            var redisSourceKeys = sourceKeys.Select(s => new RedisKey(s)).ToArray();
            var redisDestinationKey = new RedisKey(destination);

            _redisExecutor.UnionAndStore(redisDestinationKey, redisSourceKeys);
        }

        public void AddHash(string person1, Dictionary<string, object> person1Vals)
        {
            var redisKey = new RedisKey(person1);
            var hashVals = person1Vals
                .Select(d => new HashEntry(d.Key, d.Value.ToString())).ToArray();

            _redisExecutor.AddHash(redisKey, hashVals);
        }

        public IEnumerable<string> GetAllFieldsOfHash(string key)
        {
            var redisKey = new RedisKey(key);
            return _redisExecutor.GetAllFieldsOfHash(redisKey);
        }

        public void PrepareScript(string basicScript)
        {
            _redisExecutor.AddLuaScript(basicScript);
        }

        public string? EvaluateScript(object values)
        {
            return _redisExecutor.EvaluateScript(values);
        }

        public void CreateTransaction()
        {
            _redisExecutor.CreateTransaction();
        }

        public bool ExecuteTransaction()
        {
            return _redisExecutor.ExecuteTransaction();
        }

        public Task TransactionAddHash(string key, Dictionary<string, object> vals)
        {
            var redisKey = new RedisKey(key);
            var hashEntries = vals
                .Select(d => new HashEntry(d.Key, d.Value.ToString())).ToArray();

            return _redisExecutor.TransactionAddHashAsync(redisKey, hashEntries);
        }

        public Task<bool> TransactionAddSortedSet(string key, string val, int score)
        {
            var redisKey = new RedisKey(key);
            var value = new RedisValue(val);
            var redisScore = score;

            return _redisExecutor.TransactionSortedSetAddAsync(redisKey, value, redisScore);
        }
    }
}
