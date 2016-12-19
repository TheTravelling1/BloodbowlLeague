using System.Collections.Generic;
using System.Linq;
using BloodbowlLeague.Logic.Values;

namespace BloodbowlLeague.Logic.Race
{
    public class Race
    {
        private readonly List<PlayerType> _playerTypes;

        public string Name { get; }

        public IReadOnlyCollection<PlayerType> PlayerTypes => _playerTypes.AsReadOnly();

        public Race( string name )
        {
            Name = name;
            _playerTypes = new List<PlayerType>();
        }

        public Race(string name, List<PlayerType> playerTypes)
        {
            Name = name;
            _playerTypes = playerTypes;
        }

        public void AddPlayerType( string name, PlayerStats playerStats, params Skill[] skills )
        {
            _playerTypes.Add( new PlayerType( name, Name, playerStats, skills.Select( s => s.Name ) ) );
        }
    }
}