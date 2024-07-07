using Utility.Helpers;

namespace Utility.Entity
{
    public class Person : IDictionaryable
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                {"FirstName", FirstName },
                {"MiddleName", MiddleName},
                {"LastName", LastName }
            };
        }

    }
}
