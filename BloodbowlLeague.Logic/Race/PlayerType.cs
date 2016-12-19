using System.Collections.Generic;
using System.Linq;

namespace BloodbowlLeague.Logic
{
    public class PlayerType
    {
        private readonly List<string> _baseSkills;

        public string Name { get; }

        public string Race { get; }

        public PlayerStats BaseStats { get; }

        public IReadOnlyCollection<string> BaseSkills => _baseSkills.AsReadOnly();

        public PlayerType( string name, string race, PlayerStats baseStats, IEnumerable<string> baseSkills )
        {
            Name = name;
            Race = race;
            BaseStats = baseStats;
            _baseSkills = baseSkills.ToList();
        }
    }
}