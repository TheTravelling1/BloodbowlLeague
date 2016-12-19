using System.Collections.Generic;
using System.Linq;

namespace BloodbowlLeague.Logic
{
    public class Team
    {
        private readonly List<Player> _players;

        public string Name { get; }

        public string Race { get; }

        public IReadOnlyCollection<Player> Players => _players.AsReadOnly();

        public Team( string name, string race )
        {
            Name = name;
            Race = race;
            _players = new List<Player>();
        }

        public Team( string name, string race, IEnumerable<Player> players )
            : this( name, race )
        {
            _players = players.ToList();
        }

        public void AddPlayer( PlayerType type, string name )
        {
            if ( type.Race != Race )
            {
                throw new IncorrectRaceException();
            }

            _players.Add( new Player( name, type ) );
        }
    }
}
