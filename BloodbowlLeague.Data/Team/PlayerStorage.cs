namespace BloodbowlLeague.Data
{
    public class PlayerStorage
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public PlayerStatStorage Stats { get; set; }
        public PlayerStatStorage BaseStats { get; set; }
        public string[] Skills { get; set; }
        public string[] BaseSkills { get; set; }
    }
}