namespace RedisCourseRU102N.Providers
{
    public interface IRedisKeyProvider
    {
        public string CreateKeyForUser();
        public string CreateUserOnlineStatusKey(RedisKeyOptions options);
    }
}
