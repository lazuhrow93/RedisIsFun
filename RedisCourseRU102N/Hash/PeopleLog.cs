using Providers.Interfaces;
using RedisCourseRU102N.Controller;
using Providers.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.Hash
{
    public class PeopleLog
    {
        private IRedisCommandExecutor _commandExecutor;
        private IRedisKeyProvider _keyProvider;

        public PeopleLog(IRedisCommandExecutor commandExecutor, IRedisKeyProvider keyProvider)
        {
            _commandExecutor = commandExecutor;
            _keyProvider = keyProvider;
        }

        public void RunPeopleLogApp()
        {
            var person1 = _keyProvider.Create(new RedisKeyOptions() { PeopleLedgerKeyName = "person:1" });
            var person2 = _keyProvider.Create(new RedisKeyOptions() { PeopleLedgerKeyName = "person:2" });
            var person3 = _keyProvider.Create(new RedisKeyOptions() { PeopleLedgerKeyName = "person:3" });

            _commandExecutor.ClearKey(person1, person2, person3);

            var person1Vals = new Dictionary<string, object>()
            {
                { "name" , "kate"},
                { "age" , 2},
                { "email" , "kate@gmail.com"}
            };

            var person2Vals = new Dictionary<string, object>()
            {
                { "name" , "claudia"},
                { "age" , 32},
                { "email" , "claudia@gmail.com"}
            };

            var person3Vals = new Dictionary<string, object>()
            {
                { "name" , "brian"},
                { "age" , 32},
                { "email" , "brian@gmail.com"}
            };

            _commandExecutor.AddHash(person1, person1Vals);
            _commandExecutor.AddHash(person2, person2Vals);
            _commandExecutor.AddHash(person3, person3Vals);

            Console.WriteLine("Here are the fields for the hashes");

            var person1Fields = _commandExecutor.GetAllFieldsOfHash(person1);

            Console.WriteLine($"{string.Join(", ", person1Fields)}");
        }
    }
}
