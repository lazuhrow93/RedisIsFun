using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using RedisCourseRU102N.List;
using RedisCourseRU102N.Pipeline;
using RedisCourseRU102N.Providers;
using RedisCourseRU102N.Sets;
using RedisCourseRU102N.Strings;
using StackExchange.Redis;

namespace RedisCourseRU102N
{
    public class AAppController
    {
        private IRedisCommandExecutor _redisCommandExecutor;

        public AAppController(ConfigurationOptions options)
        {
            _redisCommandExecutor = new RedisCommandExecutor(options);
        }

        public async Task ControlCenter()
        {
            PromptUser();
            var answer = Console.ReadLine();

            var parsable = Int32.TryParse(answer, out int answer2);
            while(parsable == false || _possibleAnswersToPrompt.Contains(answer2) == false)
            {
                Console.WriteLine("Not a valid answer, please choose a valid answer");
                answer = Console.ReadLine();
                parsable = Int32.TryParse(answer, out answer2);
            }

            switch (answer2)
            {
                case 1 : RunPingApp();
                    break;
                case 2: await RunUnpipelinedApp();
                    break;
                case 3: await RunPipelinedApp();
                    break;
                case 4: await RunExplicitPipelineApp();
                    break;
                case 5: RunBasicGetSetString();
                    break;
                case 6: RunBasicGetSetStringWithTTL();
                    break;
                case 7: RunBasicPushLeftAndRightList();
                    break;
                case 8: RunEnumerateABasicList();
                    break;
                case 9: RunSetUnionApp();
                    break;
                default: throw new NotImplementedException();
            }

            await ControlCenter();
        }

        public void RunPingApp()
        {
            new Pinger(_redisCommandExecutor).Ping();
        }

        public async Task RunUnpipelinedApp()
        {
            await new Pipeliner(_redisCommandExecutor).UnpipelinedApp();
        }

        public async Task RunPipelinedApp()
        {
            await new Pipeliner(_redisCommandExecutor).ImplicitPipelined();
        }

        public async Task RunExplicitPipelineApp()
        {
            await new Pipeliner(_redisCommandExecutor).ExplicitPipeline();
        }

        public void RunBasicGetSetString()
        {
            new RedisSetterAndGetter(_redisCommandExecutor).BasicSetAndGetString();
        }

        public void RunBasicGetSetStringWithTTL()
        {
            new RedisSetterAndGetter(_redisCommandExecutor).BasicSetAndGetWithTTL();
        }

        public void RunBasicPushLeftAndRightList()
        {
            new PushLeftRight(_redisCommandExecutor).RunPushRightAndLeftApp();
        }

        public void RunEnumerateABasicList()
        {
            new EnumeratingLists(_redisCommandExecutor).EnumeratingTheList();
        }

        public void RunSetUnionApp()
        {
            new ActiveAndInactive(_redisCommandExecutor, new RedisKeyProvider()).RunSetsApp();
        }

        private void PromptUser()
        {
            Console.Write(_prompt);
        }

        private const string _prompt = $"" +
            $"Howdy! Welcome to the Redis Fun Server. What do you want to check out?\n" +
            $"----------------------------------------------------------------------\n" +
            $"\t1. Basic Ping Server\n" +
            $"\t2. Unpipelined Pinging\n" +
            $"\t3. Pipelined Pinging\n" +
            $"\t4. Explicit Pipelined Pinging\n" +
            $"\t5. Get and Set a String on Redis\n" +
            $"\t6. Get and Set a string on Redis with Time to live (TTL)\n" +
            $"\t7. Basic Push and Left and Right on a List\n" +
            $"\t8. Enumerating a basic list\n" +
            $"\t9. Union on a multiple sets of users\n";

        private int[] _possibleAnswersToPrompt = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }



}
