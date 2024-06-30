using Utility.Entity;

namespace Utility.Faker
{
    public class PersonFaker : MyFaker<Person>
    {
        public PersonFaker()
        {
            RuleFor(p => p.FirstName, f => f.Name.FirstName());
            RuleFor(p => p.LastName, f => f.Name.LastName());
            RuleFor(p => p.MiddleName, f => f.Random.AlphaNumeric(1).ToString());
        }
    }
}
