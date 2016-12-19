namespace BloodbowlLeague.Data
{
    public class PlayerTypeStorage
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public PlayerStatStorage BaseStats { get; set; }
        public string[] BaseSkills { get; set; }
    }
}