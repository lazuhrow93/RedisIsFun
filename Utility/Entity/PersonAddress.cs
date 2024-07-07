using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Utility.Entity
{
    public class PersonAddress : IDictionaryable
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? PostalCode { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "Name", Name },
                { "Age", Age },
                { "PostalCode", PostalCode }
            };
        }
    }
}
