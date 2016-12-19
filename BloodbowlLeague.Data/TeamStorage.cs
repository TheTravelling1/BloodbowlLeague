using BloodbowlLeague.Logic.Team;

namespace BloodbowlLeague.Data
{
    public class TeamStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public PlayerStorage[] Players { get; set; }
    }
}