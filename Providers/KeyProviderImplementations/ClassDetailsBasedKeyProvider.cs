using Providers.Interfaces;
using System.Text;

namespace Providers.KeyProviderImplementations
{
    public class ClassDetailsBasedKeyProvider : IRedisObjectKeyProvider
    {
        private const string _delimiter = ":";

        public LazRedisKey ForObject<T>(T? obj, string customKey)
        {
            var redisKey = new LazRedisKey();
            var nameOfType = obj!.GetType().Name;
            var primaryKey = $"{nameOfType}:{customKey}";
            redisKey.SetKey(primaryKey);

            var properties = obj.GetType().GetProperties();

            foreach(var property in properties)
            {
                var sb = new StringBuilder();
                sb
                    .Append($"{nameOfType}")
                    .Append($"{_delimiter}{property.Name}")
                    .Append($"{_delimiter}{property.GetValue(obj)}");
                redisKey.AddPropertyKey(property, sb.ToString());
            }
            return redisKey;
        }
    }
}
