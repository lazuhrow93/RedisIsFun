using Providers.Interfaces;

namespace RedisCourseRU102N.List
{
    public class PushLeftRight
    {
        private IRedisCommandExecutor _executor;

        public PushLeftRight(IRedisCommandExecutor executor)
        {
            _executor = executor;
        }

        public void RunPushRightAndLeftApp()
        {
            var fruits = new List<string>()
            {
                "Apple",
                "Banana",
                "Strawberry",
                "Blueberry",
                "Orange" //this will be first
            };

            var fruitKey = "myFruit";
            var vegetableKey = "myVegetable";

            _executor.ClearKey(fruitKey, vegetableKey);
            _executor.PushLeftList(fruitKey, fruits);

            var randomIndex = 0;
            var firstFruit = _executor.GetFromList(fruitKey, randomIndex);
            Console.WriteLine($"First fruit at index {randomIndex}: {firstFruit}");

            _executor.ClearKey(fruitKey);
            _executor.PushRightList(fruitKey, fruits);

            firstFruit = _executor.GetFromList(fruitKey, randomIndex);

            Console.WriteLine($"But we push right the fruit at index[{randomIndex}]: {firstFruit}");
        }
    }
}
