namespace RedisCourseRU102N.Providers
{
    public interface IRedisKeyProvider
    {
        public string MasterKey();
        public string Create(RedisKeyOptions options);
    }
}
