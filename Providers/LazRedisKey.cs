using System.Reflection;

namespace Providers
{
    public class LazRedisKey
    {
        private string? _value;

        public string Value
        {
            get
            {
                return _value ?? "";
            }
        }

        public Dictionary<string, string> PropertyKeys { get; set; }

        public LazRedisKey()
        {
            PropertyKeys = new Dictionary<string, string>();
        }

        public LazRedisKey SetKey(string key)
        {
            _value = key;
            return this;
        }

        public LazRedisKey AddPropertyKey(PropertyInfo propertyInfo, string value)
        {
            PropertyKeys!.Add(propertyInfo.Name, value);
            return this;
        }
    }
}
