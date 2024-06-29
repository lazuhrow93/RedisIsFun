using RedisCourseRU102N.ConnectingAndPing;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Controller
{
    public class Executor
    {
        RedisExecutor _redisExecutor;

        public Executor(ConfigurationOptions options)
        {
            _redisExecutor = new(options);
        }

        public void RunPingApp()
        {
            string stringContainer(DateTime startDate, TimeSpan result, DateTime endDate) => 
                $"Executing at {startDate}. Finished at {endDate}. TimeSpan {result}";

            while (true)
            {
                var startDate = DateTime.UtcNow;
                var pingResponse = _redisExecutor.Ping();
                var endDate = DateTime.UtcNow;
                Console.WriteLine(stringContainer(startDate, pingResponse,endDate));

                Thread.Sleep(1000);
            }
        }
    }
}
