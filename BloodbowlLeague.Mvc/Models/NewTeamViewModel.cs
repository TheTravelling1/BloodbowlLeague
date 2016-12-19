namespace BloodbowlLeague.Mvc.Models
{
    public class NewTeamViewModel
    {
        public string Name { get; set; }
        public string Race { get; set; }

        public string[] AvailableRaces { get; set; }
    }
}