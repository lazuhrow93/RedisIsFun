using RedisCourseRU102N.Controller;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCourseRU102N.List
{
    public class PushLeftRight
    {
        private IRedisCommandExecutor _exector;

        public PushLeftRight(IRedisCommandExecutor executor)
        {
            _exector = executor;
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

            _exector.ClearKey(fruitKey, vegetableKey);
            _exector.PushLeftList(fruitKey, fruits);

            var randomIndex = new Random().Next(0, 4);
            var firstFruit = _exector.GetFromList(fruitKey, randomIndex);
            Console.WriteLine($"First fruit at index {randomIndex}: {firstFruit}");
        }
    }
}
