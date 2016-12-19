namespace BloodbowlLeague.Logic
{
    public class PlayerStats
    {
        public byte MovementAllowance { get; }

        public byte Strength { get; }

        public byte Agility { get; }

        public byte ArmourValue { get; }

        public PlayerStats( byte movementAllowance, byte strength, byte agility, byte armourValue )
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
                var hashCode = MovementAllowance.GetHashCode();
                hashCode = (hashCode*397) ^ Strength.GetHashCode();
                hashCode = (hashCode*397) ^ Agility.GetHashCode();
                hashCode = (hashCode*397) ^ ArmourValue.GetHashCode();
                return hashCode;
            }
        }
    }
}