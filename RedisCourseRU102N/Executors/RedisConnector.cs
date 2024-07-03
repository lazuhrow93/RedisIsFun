using StackExchange.Redis;

namespace RedisCourseRU102N.Controller
{
    public class RedisConnector
    {
        public ConnectionMultiplexer Multiplexor;
        public IDatabase Redis;
        private LuaScript? _preparedLua;

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

        public void DeleteKeys(RedisKey[] keysToDelete)
        {
            Redis.KeyDelete(keysToDelete);
        }

        public void PushLeftList(RedisKey key, RedisValue[] vals)
        {
            Redis.ListLeftPush(key, vals);
        }

        public void PushRightList(RedisKey key, RedisValue[] vals)
        {
            Redis.ListRightPush(key, vals);
        }

        public string? GetFromList(RedisKey key, int index)
        {
            var element = Redis.ListGetByIndex(key, index);

            return (element.HasValue) ? element.ToString() : null;
        }

        public IEnumerable<string>? GetRange(RedisKey key, int start, int end)
        {
            var parsed = Redis.ListRange(key, start, end).Select(r => (string)r!);
            return parsed;
        }

        public void AddSet(RedisKey key, RedisValue[] vals)
        {
            Redis.SetAdd(key, vals);
        }

        public IEnumerable<string>? GetSet(RedisKey redisKey)
        {
            var redisvals = Redis.SetScan(redisKey);
            return Redis.SetScan(redisKey).Select(r => (string)r!);
        }

        public void UnionAndStore(RedisKey destination, params RedisKey[] sourceKeys)
        {
            Redis.SetCombineAndStore(SetOperation.Union, destination, sourceKeys);
        }

        public void AddHash(RedisKey redisKey, HashEntry[] hashVals)
        {
            Redis.HashSet(redisKey, hashVals);
        }

        public IEnumerable<string> GetAllFieldsOfHash(RedisKey redisKey)
        {
            return Redis.HashGetAll(redisKey).Select(h => h.ToString());
        }

        public void AddLuaScript(string script)
        {
            _preparedLua = LuaScript.Prepare(script);
        }

        public string? EvaluateScript(object values)
        {
            if (_preparedLua is null)
                throw new NullReferenceException("Lua script not defined");
            return Redis.ScriptEvaluate(_preparedLua, values).ToString();
        }
    }
}
