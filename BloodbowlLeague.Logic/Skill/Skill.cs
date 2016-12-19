﻿namespace BloodbowlLeague.Logic
{
    public class Skill
    {
        public string Name { get; }

        public string Description { get; }

        public Skill( string name, string description )
        {
            Name = name;
            Description = description;
        }
    }
}