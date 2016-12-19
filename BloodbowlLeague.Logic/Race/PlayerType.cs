using System.Collections.Generic;
using System.Linq;
using BloodbowlLeague.Logic.Values;

namespace BloodbowlLeague.Logic.Race
{
    public class PlayerType
    {
        private readonly List<Skill> _baseSkills;

        public string Name { get; }

        public Race Race { get; }

        public PlayerStats BaseStats { get; }

        public IReadOnlyCollection<Skill> BaseSkills => _baseSkills.AsReadOnly();

        public PlayerType( string name, Race race, PlayerStats baseStats, IEnumerable<Skill> skills )
        {
            Name = name;
            Race = race;
            BaseStats = baseStats;
            _baseSkills = skills.ToList();
        }
    }
}