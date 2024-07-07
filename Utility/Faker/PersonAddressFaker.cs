using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Entity;

namespace Utility.Faker
{
    public class PersonAddressFaker : MyFaker<PersonAddress>
    {
        public PersonAddressFaker()
        {
            RuleFor(p => p.Name, f => f.Person.FullName);
            RuleFor(p => p.Age, f => f.Random.Int(1, 30));
            RuleFor(p => p.PostalCode, f => f.Address.ZipCode());
        }
    }
}
