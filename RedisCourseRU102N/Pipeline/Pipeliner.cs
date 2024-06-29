using RedisCourseRU102N.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Pipeline
{
    public class Pipeliner
    {
        public IRedisCommandExecutor _redisCommandExecutor;
        
        public Pipeliner(IRedisCommandExecutor redisCommandExecutor)
        {
            _redisCommandExecutor = redisCommandExecutor;
        }

        public async Task UnpipelinedApp()
        {
            var watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                await _redisCommandExecutor.PingAsync();
            }

            watch.Stop();
            Console.WriteLine($"Elapsed Time for 1000 unpipeline pings: {watch.Elapsed}");
        }

        public async Task ImplicitPipelined()
        {
            var listOfTasks = new List<Task<TimeSpan>>();
            var watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                listOfTasks.Add(_redisCommandExecutor.PingAsync());
            }

            await Task.WhenAll(listOfTasks);

            Console.WriteLine($"Elapsed Time for 1000 unpipeline pings: {watch.Elapsed}");
        }

        public async Task ExplicitPipeline()
        {
            _redisCommandExecutor.StartBatch();

            var listOfTasks = new List<Task<TimeSpan>>();
            var watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                listOfTasks.Add(_redisCommandExecutor.PingAsyncOnBatch());
            }

            _redisCommandExecutor.ExecuteBatch();

            await Task.WhenAll(listOfTasks);
            watch.Stop();

            Console.WriteLine($"Elapsed Time for 1000 unpipeline pings: {watch.Elapsed}");
        }
    }
}
