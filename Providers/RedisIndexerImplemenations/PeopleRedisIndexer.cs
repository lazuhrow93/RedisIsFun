using Providers.Interfaces;
using System.Reflection;
using Utility.Helpers;

namespace Providers.RedisIndexerImplemenations
{
    public class PeopleRedisIndexer : IRedisIndexer
    {
        private IRedisCommandExecutor _redisCommandExecutor;
        private IRedisObjectKeyProvider _keyProvider;

        public PeopleRedisIndexer(IRedisCommandExecutor redisCommandExecutor,
            IRedisObjectKeyProvider keyProvider)
        {
            _redisCommandExecutor = redisCommandExecutor;
            _keyProvider = keyProvider;
        }

        public bool AddToHash<T>(string key, T? entity)
            where T : IDictionaryable
        {
            if (entity == null) return false;

            LazRedisKey keyForObject = _keyProvider.ForObject<T>(entity, key);
            var propertyKeys = keyForObject.PropertyKeys;
            var entryKey = keyForObject.Value;

            _redisCommandExecutor.CreateTransaction();
            _redisCommandExecutor.TransactionAddHash(entryKey, entity.ToDictionary());

            var entityProperties = entity.GetType().GetProperties();
            foreach(var property in entityProperties)
            {
                if (!propertyKeys.TryGetValue(property.Name, out string? propertyKey))
                    throw new NotImplementedException($"Something is wrong with property {property.Name}. A key was not made for it in the provier");

                var propertyValue = property.GetValue(entity);
                if (propertyValue == null)
                    continue;
                int redisScore = (property.GetType() == typeof(int)) ? (int)propertyValue : 0;
                _redisCommandExecutor.TransactionAddSortedSet(propertyKey, entryKey, redisScore);
            }

            _redisCommandExecutor.ExecuteTransaction();
            return true;
        }
    }
}
