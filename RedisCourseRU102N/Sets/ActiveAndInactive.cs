using Utility.Faker;
using static Providers.KeyEnum;
using Providers.Interfaces;
using Providers.Options;

namespace RedisCourseRU102N.Sets
{
    public class ActiveAndInactive
    {
        IRedisCommandExecutor _executor;
        IRedisKeyProvider _keyProvider;

        public ActiveAndInactive(IRedisCommandExecutor executor, IRedisKeyProvider provider)
        {
            _executor = executor;
            _keyProvider = provider;
        }

        public void RunSetsApp()
        {
            var keyOptions = new RedisKeyOptions();
            var usersMasterKey = _keyProvider.MasterKey();

            keyOptions.UserOnlineStatus = UserOnlineStatus.Active;
            var activeUserKey = _keyProvider.Create(keyOptions);
            keyOptions.UserOnlineStatus = UserOnlineStatus.InActive;
            var inactiveUserKey = _keyProvider.Create(keyOptions);
            keyOptions.UserOnlineStatus = UserOnlineStatus.Online;
            var onlineUserKey = _keyProvider.Create(keyOptions);
            keyOptions.UserOnlineStatus = UserOnlineStatus.Offline;
            var offlineUserKey = _keyProvider.Create(keyOptions);

            _executor.ClearKey(usersMasterKey,
                activeUserKey,
                inactiveUserKey,
                onlineUserKey,
                offlineUserKey);

            var peopleFaker = new PersonFaker();
            var users = peopleFaker.Generate(8).Select(s => s.FirstName).ToList();

            var activeUsers = users.GetRange(0, 2);
            var inactiveUsers = users.GetRange(2, 2);
            var onlineUsers = users.GetRange(4, 2);
            var offlineUsers = users.GetRange(6, 2);

            _executor.AddSet(activeUserKey, activeUsers!);
            _executor.AddSet(inactiveUserKey, inactiveUsers!);
            _executor.AddSet(onlineUserKey, onlineUsers!);
            _executor.AddSet(offlineUserKey, offlineUsers!);

            var currentAllUsers = _executor.GetSet(usersMasterKey);
            Console.WriteLine($"Current Total User before union: {string.Join(", ", currentAllUsers!)}");

            _executor.UnionAndStore(
                usersMasterKey,
                activeUserKey,
                inactiveUserKey,
                onlineUserKey,
                offlineUserKey);

            currentAllUsers = _executor.GetSet(usersMasterKey);
            Console.WriteLine($"Total Users after union: {string.Join(", ", currentAllUsers!)}");
        }
    }
}
