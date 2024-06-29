namespace RedisCourseRU102N.Controller
{
    public interface IRedisCommandExecutor
    {
        public TimeSpan Ping();
        public Task<TimeSpan> PingAsync();
        public Task<TimeSpan> PingAsyncOnBatch();
        void StartBatch();
        void ExecuteBatch();
    }
}
