using System.Collections.Generic;

namespace BloodbowlLeague.Logic.Values
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

        protected bool Equals( Skill other )
        {
            return string.Equals( Name, other.Name ) && string.Equals( Description, other.Description );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType() != this.GetType() ) return false;
            return Equals( (Skill) obj );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ( (Name?.GetHashCode() ?? 0) * 397 ) ^ (Description?.GetHashCode() ?? 0);
            }
        }
    }
}