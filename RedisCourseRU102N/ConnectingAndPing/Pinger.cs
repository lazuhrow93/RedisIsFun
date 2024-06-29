using StackExchange.Redis;

namespace RedisCourseRU102N.ConnectingAndPing
{
    public class Pinger
    {
        ConnectionMultiplexer _connectionMultiplexer;
        IDatabase _redisClient;

        public Pinger(ConfigurationOptions ops)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(ops);
            _redisClient = _connectionMultiplexer.GetDatabase();
        }

        public void Ping()
        {
            var result = _redisClient.Ping();

            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(result);
            }
        }
    }
}
