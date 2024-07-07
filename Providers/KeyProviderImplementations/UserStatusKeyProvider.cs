using Providers.Interfaces;
using Providers.Options;

namespace Providers.KeyProviderImplementations
{
    public class UserStatusKeyProvider : IRedisKeyProvider
    {
        private const string _root = "user";
        private const string Delimeter = ":";

        public string MasterKey()
        {
            return _root;
        }

        public string Create(RedisKeyOptions options)
        {
            var keyBuilder = AppendKey(_root, "state");
            keyBuilder = AppendKey(keyBuilder, options.UserOnlineStatus.ToString());
            return keyBuilder;
        }

        private string AppendKey(string source, string key)
        {
            return $"{source}{Delimeter}{key}";
        }

        public List<LazRedisKey> Create<T>()
        {
            throw new NotImplementedException();
        }
    }
}
