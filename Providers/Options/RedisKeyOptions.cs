using static Providers.KeyEnum;

namespace Providers.Options
{
    public class RedisKeyOptions
    {
        public UserOnlineStatus UserOnlineStatus { get; set; }
        public string? PeopleLedgerKeyName { get; set; }
    }
}
