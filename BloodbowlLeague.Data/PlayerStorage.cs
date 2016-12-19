namespace BloodbowlLeague.Data
{
    public class PlayerStorage
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public StatStorage Stats { get; set; }
        public StatStorage BaseStats { get; set; }
        public SkillStorage[] Skills { get; set; }
        public SkillStorage[] BaseSkills { get; set; }
    }
}