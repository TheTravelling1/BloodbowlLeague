namespace BloodbowlLeague.Logic.Team
{
    public interface ITeamRepository
    {
        void Save( Team toSave );
        Team Get(string teamName);
    }
}
