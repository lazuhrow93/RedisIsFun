using Providers.Options;
using System.Reflection;

namespace Providers.Interfaces
{
    public interface IRedisKeyProvider
    {
        public string MasterKey();
        public string Create(RedisKeyOptions options);
    }
}
