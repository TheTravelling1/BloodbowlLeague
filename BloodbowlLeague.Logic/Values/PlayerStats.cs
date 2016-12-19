namespace BloodbowlLeague.Logic.Values
{
    public class PlayerStats
    {
        public int MovementAllowance { get; }

        public int Strength { get; }

        public int Agility { get; }

        public int ArmourValue { get; }

        public PlayerStats( int movementAllowance, int strength, int agility, int armourValue )
        {
            MovementAllowance = movementAllowance;
            Strength = strength;
            Agility = agility;
            ArmourValue = armourValue;
        }

        public PlayerStats Clone()
        {
            return new PlayerStats( MovementAllowance, Strength, Agility, ArmourValue );
        }

        protected bool Equals(PlayerStats other)
        {
            return MovementAllowance == other.MovementAllowance && Strength == other.Strength && Agility == other.Agility && ArmourValue == other.ArmourValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayerStats) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MovementAllowance;
                hashCode = (hashCode*397) ^ Strength;
                hashCode = (hashCode*397) ^ Agility;
                hashCode = (hashCode*397) ^ ArmourValue;
                return hashCode;
            }
        }
    }
}