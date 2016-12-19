namespace BloodbowlLeague.Data
{
    public class RaceStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerTypeStorage[] PlayerTypes { get; set; }
    }
}
