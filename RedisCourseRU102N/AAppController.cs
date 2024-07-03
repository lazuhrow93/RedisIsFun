using Providers.KeyProviderImplementations;
using RedisCourseRU102N.ConnectingAndPing;
using RedisCourseRU102N.Controller;
using RedisCourseRU102N.Hash;
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
            while(parsable == false || _options.Contains(answer2) == false)
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
                case 10: RunPeopleLogApp();
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
            new ActiveAndInactive(_redisCommandExecutor, new UserStatusKeyProvider()).RunSetsApp();
        }

        public void RunPeopleLogApp()
        {
            new PeopleLog(_redisCommandExecutor, new PeopleLedgerKeyProvider()).RunPeopleLogApp();
        }

        private void PromptUser()
        {
            Console.Write(_prompt);
        }
        
        private string _prompt
        {
            get
            {
                return $"" +
                        $"Howdy! Welcome to the Redis Fun Server. What do you want to check out?\n" +
                        $"----------------------------------------------------------------------\n" +
                        $"{PromptEnty(_options[0], "Basic Ping Server")}" +
                        $"{PromptEnty(_options[1], "Unpipelined Pinging")}" +
                        $"{PromptEnty(_options[2], "Pipelined Pinging")}" +
                        $"{PromptEnty(_options[3], "Explicit Pipelined Pinging")}" +
                        $"{PromptEnty(_options[4], "Get and Set a String on Redis")}" +
                        $"{PromptEnty(_options[5], "Get and Set a string on Redis with Time to live (TTL)")}" +
                        $"{PromptEnty(_options[6], "Basic Push and Left and Right on a List")}" +
                        $"{PromptEnty(_options[7], "Enumerating a basic list")}" +
                        $"{PromptEnty(_options[8], "Union on a multiple sets of users")}" +
                        $"{PromptEnty(_options[9], "Creating a Hashset")}";
            }
        }
        private int[] _options = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 , 10};

        private string PromptEnty(int option, string name)
            => $"\t{option}. {name}\n";
    }
}
