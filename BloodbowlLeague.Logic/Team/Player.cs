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

        public IReadOnlyCollection<Skill> BaseSkills { get; }

        public PlayerStats Stats { get; }

        public IReadOnlyCollection<Skill> Skills { get; }

        public Player( string name, PlayerType type )
        {
            Name = name;
            Type = type.Name;

            BaseStats = type.BaseStats.Clone();
            BaseSkills = new List<Skill>( type.BaseSkills );

            Stats = type.BaseStats.Clone();
            Skills = new List<Skill>( type.BaseSkills );
        }

        public Player( string name, string type, PlayerStats baseStats, IEnumerable<Skill> baseSkills, PlayerStats currentStats, IEnumerable<Skill> currentSkills )
        {
            Name = name;
            Type = type;

            BaseStats = baseStats;
            BaseSkills = baseSkills.ToList();

            Stats = currentStats;
            Skills = currentSkills.ToList();
        }
    }
}