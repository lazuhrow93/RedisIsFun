using Utility.Helpers;

namespace Providers.Interfaces
{
    public interface IRedisIndexer
    {
        bool AddToHash<T>(string key, T? entity)
            where T : IDictionaryable;
    }
}
