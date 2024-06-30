using RedisCourseRU102N.Controller;
using Utility.Faker;

namespace RedisCourseRU102N.List
{
    public class EnumeratingLists
    {
        private IRedisCommandExecutor _executor;

        public EnumeratingLists(IRedisCommandExecutor executor)
        {
            _executor = executor;
        }

        public void EnumeratingTheList()
        {
            var faker = new PersonFaker();
            var listOfNames = faker.Generate(10).Select(p => p.FirstName);

            var keyOfNames = "firstPeople";

            _executor.PushRightList(keyOfNames, listOfNames);

            var subSetOfPeople = _executor.GetRange(keyOfNames, 5, 9);
            var all = _executor.GetRange(keyOfNames, 0, 9);
            Console.WriteLine($"All: {string.Join(", ", all)}");
            Console.WriteLine($"Subset: {string.Join(", ", subSetOfPeople)}");
        }

    }
}
