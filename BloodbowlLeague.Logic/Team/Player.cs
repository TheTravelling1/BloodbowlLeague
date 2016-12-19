using System.Collections.Generic;
using System.Linq;
using BloodbowlLeague.Logic.Race;
using BloodbowlLeague.Logic.Values;

namespace BloodbowlLeague.Logic.Team
{
    public class Player
    {
        public string Name { get; }

        public string Type { get; }

        public PlayerStats BaseStats { get; }

        public IReadOnlyCollection<string> BaseSkills { get; }

        public PlayerStats Stats { get; }

        public IReadOnlyCollection<string> Skills { get; }

        public Player( string name, PlayerType type )
        {
            Name = name;
            Type = type.Name;

            BaseStats = type.BaseStats.Clone();
            BaseSkills = new List<string>( type.BaseSkills );

            Stats = type.BaseStats.Clone();
            Skills = new List<string>( type.BaseSkills );
        }

        public Player( string name, string type, PlayerStats baseStats, IEnumerable<string> baseSkills, PlayerStats stats, IEnumerable<string> skills )
        {
            Name = name;
            Type = type;

            BaseStats = baseStats;
            BaseSkills = baseSkills.ToList();

            Stats = stats;
            Skills = skills.ToList();
        }
    }
}