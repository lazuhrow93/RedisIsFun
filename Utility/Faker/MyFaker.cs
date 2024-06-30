
using Bogus;

namespace Utility.Faker
{
    public class MyFaker<T> : Faker<T>
        where T : class
    {
        public MyFaker()
        {
            Randomizer.Seed = new Random(23516);
        }
    }
}
