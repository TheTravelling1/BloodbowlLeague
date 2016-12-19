using System.Collections.Generic;
using BloodbowlLeague.Logic.Race;

namespace BloodbowlLeague.Logic.Team
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

        public void AddPlayer( PlayerType type, string name )
        {
            if ( type.Race.Name != Race )
            {
                throw new IncorrectRaceException();
            }

            _players.Add( new Player( name, type ) );
        }
    }
}
