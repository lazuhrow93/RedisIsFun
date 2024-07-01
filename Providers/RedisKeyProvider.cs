namespace RedisCourseRU102N.Providers
{
    public class RedisKeyProvider : IRedisKeyProvider
    {
        private const string _delimiter = ":";

        public string CreateKeyForUser()
        {
            return "user";
        }

        public string CreateUserOnlineStatusKey(RedisKeyOptions options)
        {
            var keyBuilder = CreateKeyForUser();
            keyBuilder += AddKeyPath("state");
            keyBuilder += AddKeyPath(options.Status.ToString());
            return keyBuilder;

        }

        private string AddKeyPath(string val)
        {
            return $"{_delimiter}{val}";
        }
    }
}
