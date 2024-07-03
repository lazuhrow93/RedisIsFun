using static Providers.KeyEnum;

namespace RedisCourseRU102N.Providers
{
    public class RedisKeyOptions
    {
        public UserOnlineStatus UserOnlineStatus { get; set; }
        public string? PeopleLedgerKeyName { get; set; }
    }
}
