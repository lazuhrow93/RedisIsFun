namespace RedisCourseRU102N.Providers
{
    public class RedisKeyOptions
    {
        public enum UserOnlineStatus
        {
            Active, InActive, Online, Offline
        }

        public UserOnlineStatus Status { get; set; }

    }
}
