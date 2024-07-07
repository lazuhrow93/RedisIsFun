using Providers.Interfaces;
using Providers.RedisIndexerImplemenations;
using Utility.Faker;

namespace RedisCourseRU102N.Transactions
{
    public class TransactionsExersice
    {
        private IRedisIndexer _redisIndexer;

        public TransactionsExersice(IRedisCommandExecutor redisCommandExecutor,
            IRedisObjectKeyProvider objectKeyProvider)
        {
            _redisIndexer = new PeopleRedisIndexer(redisCommandExecutor, objectKeyProvider);
        }

        public void RunSimpleTransactionApp()
        {
            var personAddyFaker = new PersonAddressFaker();
            var personAddy1 = personAddyFaker.Generate();

            var resultOfIndex = _redisIndexer.AddToHash("1", personAddy1);
        }

        internal void RunConditionalTransactionApp()
        {
            var personAddyFaker = new PersonAddressFaker();
            var personAddy1 = personAddyFaker.Generate();

            var resultOfIndex = _redisIndexer.AddToHash("1", personAddy1);

        }
    }
}
