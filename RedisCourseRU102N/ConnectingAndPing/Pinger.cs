using Providers.Interfaces;
using RedisCourseRU102N.Controller;
using StackExchange.Redis;

namespace RedisCourseRU102N.ConnectingAndPing
{
    public class Pinger
    {
        IRedisCommandExecutor _executor;

        public Pinger(IRedisCommandExecutor redisExecutor)
        {
            _executor = redisExecutor;
        }

        public void Ping()
        {
            string stringContainer(DateTime startDate, TimeSpan result, DateTime endDate) =>
                $"Executing at {startDate}. Finished at {endDate}. TimeSpan {result}";

            while (true)
            {
                var startDate = DateTime.UtcNow;
                var pingResponse = _executor.Ping();
                var endDate = DateTime.UtcNow;
                Console.WriteLine(stringContainer(startDate, pingResponse, endDate));

                Thread.Sleep(1000);
            }
        }
    }
}
