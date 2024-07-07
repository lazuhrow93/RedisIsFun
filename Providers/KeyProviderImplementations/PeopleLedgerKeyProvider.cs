using Providers.Interfaces;
using Providers.Options;

namespace Providers.KeyProviderImplementations
{
    public class PeopleLedgerKeyProvider : IRedisKeyProvider
    {
        private const string _root = "people";
        private const string _delimiter = ":";

        public string MasterKey()
        {
            return _root;
        }

        public string Create(RedisKeyOptions options)
        {
            return AppendKey(_root, options.PeopleLedgerKeyName ?? "");
        }

        private string AppendKey(string source, string key)
        {
            return $"{source}{_delimiter}{key}";
        }

        public List<LazRedisKey> Create<T>()
        {
            throw new NotImplementedException();
        }
    }
}
