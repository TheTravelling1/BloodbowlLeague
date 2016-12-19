namespace BloodbowlLeague.Logic.Team
{
    public interface ITeamRepository
    {
        void SaveTeam( Team toSave );
        Team Get(string teamName);
    }
}
