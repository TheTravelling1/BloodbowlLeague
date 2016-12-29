using System.Collections.Generic;
using System.Linq;

namespace BloodbowlLeague.Logic
{
    public class Coach
    {
        public string Name { get; private set; }

        public string EmailAddress { get; private set; }

        public byte[] HashedPassword { get; private set; }

        public IReadOnlyCollection<string> Teams { get; }

        public Coach( string name, string emailAddress, byte[] hashedPassword )
        {
            Name = name;
            EmailAddress = emailAddress;
            HashedPassword = hashedPassword;
        }

        public Coach( string name, string emailAddress, byte[] hashedPassword, IEnumerable<string> teams )
        {
            Teams = teams?.ToList() ?? new List<string>();
            Name = name;
            EmailAddress = emailAddress;
            HashedPassword = hashedPassword;
        }
    }
}
