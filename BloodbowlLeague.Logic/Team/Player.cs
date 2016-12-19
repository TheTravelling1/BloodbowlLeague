using System.Collections.Generic;
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
    }
}